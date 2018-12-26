using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations;
using AutoUpdate.Core.Model.Executor;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdaterManagementService : IUpdaterManagementService
    {
        private readonly IVersionSource _versionSource;
        private readonly ICurrentVersionDeterminer _currentVersionDeterminer;
        private readonly IUserInteraction[] _userInteractions;
        private readonly IUpdatePreparationStep[] _prepareSteps;
        private readonly UpdaterManagementServiceCheckStrategy _updaterCheckStrategy;
        private readonly SemaphoreSlim _semaphore;

        public bool IsActive => _semaphore.CurrentCount <= 0;

        public UpdaterManagementService(UpdaterManagementServiceConfiguration configuration)
        {
            _semaphore = new SemaphoreSlim(1, 1);

            _versionSource = configuration.VersionSource;
            _currentVersionDeterminer = configuration.CurrentVersionDeterminer;
            _userInteractions = configuration.UserInteraction.ToArray();
            _prepareSteps = configuration.UpdatePreparationSteps.ToArray();

            _updaterCheckStrategy = configuration.CheckInterval.Accept(new SelectStrategyVisitor(this));
        }

        public void Start()
            => _updaterCheckStrategy.Start();

        public Task<IUpdateVersionHandle> SearchVersion()
        {
            _semaphore.Wait();
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    IUpdateVersionHandle handle = new UpdateVersionHandle(null, this);
                    var versions = _versionSource.LoadAvailableVersions();

                    if (versions.Any())
                    {
                        var currentVersion = _currentVersionDeterminer.DetermineCurrentVersionNumber();
                        var latestVersion = versions.OrderByDescending(x => x.VersionNumber).First();

                        if (currentVersion < latestVersion.VersionNumber)
                        {
                            handle = new UpdateVersionHandle(latestVersion, this);

                            foreach (var userInteraction in _userInteractions)
                            {
                                userInteraction.NewVersionAvailable(handle);
                            }
                        }
                    }

                    return handle;
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }

        internal void UpdateVersion(UpdateVersionHandle handle)
        {
            if (!handle.HasNewVersion) return;

            var updateFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            updateFolder.Create();

            var workspace = new UpdatePreparationWorkspaceInformation(handle.NewVersion, updateFolder);
            var executorConfiguration = new ExecutorConfiguration();

            executorConfiguration.Steps = _prepareSteps.SelectMany(x => x.Prepare(workspace))
                                                       .ToArray();

            CopyAndStartExecutor(workspace, executorConfiguration);

            System.Environment.Exit(0);
            //TODO Shutdown current application
        }

        private void CopyAndStartExecutor(UpdatePreparationWorkspaceInformation workspace,
                                          ExecutorConfiguration config)
        {
            var executorDirectory = Path.Combine(workspace.WorkingDirectory.FullName, "Executor");
            var executorFile = new FileInfo(Path.Combine(executorDirectory, "AutoUpdate.Executor.exe"));

            using (var executorStream = GetExecutorStream())
            {
                var archive = new ZipArchive(executorStream);
                archive.ExtractToDirectory(executorDirectory);
            }

            executorFile.Refresh();
            if (!executorFile.Exists)
            {
                throw new InvalidOperationException("Preparation of executor helper was not successfull");
            }

            var startInfo = new ProcessStartInfo(executorFile.FullName);
            var result = Process.Start(startInfo);
        }

        private Stream GetExecutorStream()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new NotImplementedException("Other platforms than Windows currently not supported");
            }

            var path = "AutoUpdate.Core.Executor.ExecutorWin86.zip";
            if (Environment.Is64BitOperatingSystem)
            {
                path = "AutoUpdate.Core.Executor.ExecutorWin64.zip";
            }
            return GetType().Assembly.GetManifestResourceStream(path);
        }

        class SelectStrategyVisitor : IUpdaterCheckIntervalConfigurationVisitor<UpdaterManagementServiceCheckStrategy>
        {
            private readonly UpdaterManagementService _service;

            public SelectStrategyVisitor(UpdaterManagementService service)
            {
                _service = service;
            }

            public UpdaterManagementServiceCheckStrategy Handle(UpdaterOneTimeCheckIntervalConfiguration updaterOneTimeCheckIntervalConfiguration)
                => new UpdaterManagementServiceOneTimeCheckStrategy(_service);
        }
    }
}
