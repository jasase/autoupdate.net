using System;
using System.Linq;
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


        public UpdaterManagementService(UpdaterManagementServiceConfiguration configuration)
        {
            _versionSource = configuration.VersionSource;
            _currentVersionDeterminer = configuration.CurrentVersionDeterminer;
            _userInteractions = configuration.UserInteraction.ToArray();
        }

        public void Start()
        {
            var t = 0;
            throw new System.NotImplementedException();
        }

        public Task<IUpdateVersionHandle> SearchVersion()
        =>
            Task.Factory.StartNew(() =>
            {
                IUpdateVersionHandle handle = null;
                var versions = _versionSource.LoadAvailableVersions();

                if (versions.Any())
                {
                    var currentVersion = _currentVersionDeterminer.DetermineCurrentVersionNumber();
                    var latestVersion = versions.OrderByDescending(x => x.VersionNumber).First();

                    throw new NotImplementedException();

                    //if(currentVersion < latestVersion.VersionNumber)
                    //{

                    //}
                }


                return handle;
            });

    }
}
