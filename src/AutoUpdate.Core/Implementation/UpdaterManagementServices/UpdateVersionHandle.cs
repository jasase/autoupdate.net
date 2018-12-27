using System;
using AutoUpdate.Core.Abstraction;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdateVersionHandle : IUpdateVersionHandle
    {
        private readonly Model.Version _version;
        private UpdaterManagementService _updaterManagementService;

        public UpdateVersionHandle(Model.Version version,
                                    UpdaterManagementService updaterManagementService)
        {
            _version = version;
            _updaterManagementService = updaterManagementService;
        }

        public bool HasNewVersion => _version != null;
        public Model.Version NewVersion => _version;

        public void UpdateToNewVersion()
            => _updaterManagementService.UpdateVersion(this);
    }
}
