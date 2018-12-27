namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public class UpdaterManagementServiceManualCheckStrategy : UpdaterManagementServiceCheckStrategy
    {
        public UpdaterManagementServiceManualCheckStrategy(UpdaterManagementService updaterManagementService)
            : base(updaterManagementService)
        { }

        public override void Start()
        { }
    }
}
