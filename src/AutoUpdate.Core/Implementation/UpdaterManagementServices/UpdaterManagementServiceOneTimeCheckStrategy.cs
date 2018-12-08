namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdaterManagementServiceOneTimeCheckStrategy : UpdaterManagementServiceCheckStrategy
    {
        public UpdaterManagementServiceOneTimeCheckStrategy(UpdaterManagementService updaterManagementService)
            : base(updaterManagementService)
        {
        }

        public override void Start()
            => UpdaterManagementService.SearchVersion();
    }
}
