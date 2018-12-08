namespace AutoUpdate.Core.Implementation.UpdaterManagementServices
{
    public abstract class UpdaterManagementServiceCheckStrategy
    {
        public UpdaterManagementServiceCheckStrategy(UpdaterManagementService updaterManagementService)
        {
            UpdaterManagementService = updaterManagementService;
        }

        public UpdaterManagementService UpdaterManagementService { get; }

        public abstract void Start();
    }
}
