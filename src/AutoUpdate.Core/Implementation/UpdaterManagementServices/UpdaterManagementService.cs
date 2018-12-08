using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdaterManagementService : IUpdaterManagementService
    {
        private readonly IVersionSource _versionSource;
        private readonly ICurrentVersionDeterminer _currentVersionDeterminer;
        private readonly IUserInteraction[] _userInteractions;
        private readonly UpdaterManagementServiceCheckStrategy _updaterCheckStrategy;
        private readonly SemaphoreSlim _semaphore;

        public bool IsActive => _semaphore.CurrentCount <= 0;

        public UpdaterManagementService(UpdaterManagementServiceConfiguration configuration)
        {
            _semaphore = new SemaphoreSlim(1, 1);

            _versionSource = configuration.VersionSource;
            _currentVersionDeterminer = configuration.CurrentVersionDeterminer;
            _userInteractions = configuration.UserInteraction.ToArray();

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
