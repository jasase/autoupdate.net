using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.Downloaders;
using AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations;
using AutoUpdate.Shared;
using AutoUpdate.Shared.Configurations;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdaterManagementService : IUpdaterManagementService
    {
        private readonly ILogger<UpdaterManagementService> _logger;

        private readonly IVersionSource _versionSource;
        private readonly ICurrentVersionDeterminer _currentVersionDeterminer;
        private readonly IApplicationCloser _applicationCloser;

        private readonly IUserInteraction[] _userInteractions;
        private readonly IUpdatePreparationStep[] _prepareSteps;

        private readonly UpdaterManagementServiceCheckStrategy _updaterCheckStrategy;
        private readonly SemaphoreSlim _semaphore;

        public bool IsActive => _semaphore.CurrentCount <= 0;

        public UpdaterManagementService(ILoggerFactory loggerFactory,
                                        UpdaterManagementServiceConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<UpdaterManagementService>();
            _semaphore = new SemaphoreSlim(1, 1);

            _versionSource = configuration.VersionSource;
            _currentVersionDeterminer = configuration.CurrentVersionDeterminer;
            _applicationCloser = configuration.ApplicationCloser;
            _userInteractions = configuration.UserInteraction.ToArray();
            _prepareSteps = configuration.UpdatePreparationSteps.ToArray();

            _updaterCheckStrategy = configuration.CheckInterval.Accept(new SelectStrategyVisitor(this));
        }

        public void Start()
            => _updaterCheckStrategy.Start();

        public Task<IUpdateVersionHandle> SearchVersion()
        {
            _logger.LogTrace("Starting searching new version was triggerd. Waiting other operation finished");
            _semaphore.Wait();
            _logger.LogTrace("Lock accuired. Starting searching task");

            return Task.Factory.StartNew(() =>
            {
                _logger.LogInformation("Starting searching new version");
                try
                {
                    IUpdateVersionHandle handle = new UpdateVersionHandle(null, this);
                    var versions = _versionSource.LoadAvailableVersions();

                    if (versions.Any())
                    {
                        _logger.LogDebug("Available version: {0}", string.Join(Environment.NewLine, versions.Select(x => x.ToString())));
                        var currentVersion = _currentVersionDeterminer.DetermineCurrentVersionNumber();
                        var latestVersion = versions.OrderByDescending(x => x.VersionNumber).First();

                        if (currentVersion < latestVersion.VersionNumber)
                        {
                            _logger.LogInformation("New version found. Latest [{0}] Current [{1}]", latestVersion, currentVersion);
                            handle = new UpdateVersionHandle(latestVersion, this);

                            foreach (var userInteraction in _userInteractions)
                            {
                                userInteraction.NewVersionAvailable(handle);
                            }
                        }
                        else
                        {
                            _logger.LogInformation("No new version found. Latest [{0}] Current [{1}]", latestVersion, currentVersion);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No versions found");
                    }

                    return handle;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during searching new version occurred");
                    throw;
                }
                finally
                {
                    _logger.LogTrace("Releasing lock. Searching new version");
                    _semaphore.Release();
                }
            });
        }

        internal void UpdateVersion(UpdateVersionHandle handle)
        {
            if (!handle.HasNewVersion) return;

            _logger.LogTrace("Starting update to version [{0}]. Waiting other operation finished", handle.NewVersion);
            _semaphore.Wait();
            _logger.LogTrace("Lock accuired. Starting update");

            try
            {
                _logger.LogInformation("Update to version [{0}] started", handle.NewVersion);

                var updateFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "AutoUpdate.Net", Guid.NewGuid().ToString()));
                _logger.LogDebug("Creating working folder '{0}'", updateFolder.FullName);
                updateFolder.Create();

                var artifactsFolder = new DirectoryInfo(Path.Combine(updateFolder.FullName, "Artifacts"));
                _logger.LogDebug("Creating artifacts folder '{0}'", artifactsFolder.FullName);
                artifactsFolder.Create();

                var workspace = new UpdatePreparationWorkspaceInformation(handle.NewVersion, updateFolder, artifactsFolder);
                var executorConfiguration = new ExecutorConfiguration();

                var downloader = handle.NewVersion.Source.Accept(new DownloaderFactory());
                downloader.Download(workspace);

                executorConfiguration.Steps = _prepareSteps.SelectMany(x => x.Prepare(workspace))
                                                           .ToArray();

                var curProcess = Process.GetCurrentProcess();
                executorConfiguration.Application.Path = curProcess.MainModule.FileName;
                executorConfiguration.Application.RestartArguments = Environment.CommandLine;
                executorConfiguration.Application.CallingProcessId = Process.GetCurrentProcess().Id;

                _logger.LogDebug("Copy update executor application to working folder");
                CopyAndStartExecutor(workspace, executorConfiguration);

                _logger.LogDebug("Executor started. Closing current application");
                _applicationCloser.CloseApplication();
            }
            finally
            {
                _logger.LogTrace("Releasing lock. Updating");
                _semaphore.Release();
            }
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

            var serializer = new ConfigurationSerializer();
            var serializedConfig = serializer.Serialize(config);

            var configFile = new FileInfo(Path.Combine(executorDirectory, ExecutorConfiguration.DEFAULT_FILENAME));
            using (var configFileStream = configFile.OpenWrite())
            using (var configFileWriter = new StreamWriter(configFileStream))
            {
                configFileWriter.Write(serializedConfig);
            }

            var startInfo = new ProcessStartInfo(executorFile.FullName, configFile.FullName);
            startInfo.WorkingDirectory = executorDirectory;
            startInfo.UseShellExecute = false;
            var result = Process.Start(startInfo);
        }

        private Stream GetExecutorStream()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new NotImplementedException("Other platforms than Windows currently not supported");
            }

            var path = "AutoUpdate.Core.Executors.ExecutorWin86.zip";
            if (Environment.Is64BitOperatingSystem)
            {
                path = "AutoUpdate.Core.Executors.ExecutorWin64.zip";
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

            public UpdaterManagementServiceCheckStrategy Handle(UpdaterManualCheckIntervalConfiguration configuration)
                => new UpdaterManagementServiceManualCheckStrategy(_service);
        }
    }
}
