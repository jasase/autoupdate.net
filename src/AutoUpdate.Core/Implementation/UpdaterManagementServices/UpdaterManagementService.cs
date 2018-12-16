using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        }

        private void CopyAndStartExecutor(UpdatePreparationWorkspaceInformation workspace,
                                          ExecutorConfiguration config)
        {
            var executorFile = new FileInfo(Path.Combine(workspace.WorkingDirectory.FullName, "Executor.exe"));
            using (var executorStream = GetType().Assembly.GetManifestResourceStream("AutoUpdate.Core.AutoUpdate.Executor.dll"))
            using (var destStream = executorFile.OpenWrite())
            {
                executorStream.CopyTo(destStream);
            }

            var startInfo = new ProcessStartInfo("dotnet", executorFile.FullName);

            var result = Process.Start(startInfo);
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
