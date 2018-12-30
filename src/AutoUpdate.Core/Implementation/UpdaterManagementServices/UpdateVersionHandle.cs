using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdateVersionHandle : IUpdateVersionHandle
    {
        private readonly Version _version;
        private UpdaterManagementService _updaterManagementService;

        public UpdateVersionHandle(Version version,
                                   UpdaterManagementService updaterManagementService)
        {
            _version = version;
            _updaterManagementService = updaterManagementService;
        }

        public bool HasNewVersion => _version != null;
        public Version NewVersion => _version;

        public void UpdateToNewVersion()
            => _updaterManagementService.UpdateVersion(this);
    }
}
