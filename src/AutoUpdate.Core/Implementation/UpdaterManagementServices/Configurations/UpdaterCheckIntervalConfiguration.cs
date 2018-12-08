namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public abstract class UpdaterCheckIntervalConfiguration
    {
        public abstract TReturn Accept<TReturn>(IUpdaterCheckIntervalConfigurationVisitor<TReturn> visitor);
        public abstract UpdaterManagementServiceConfigurationValidationMessage Validate();
    }
}
