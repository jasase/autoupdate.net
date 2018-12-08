namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public class UpdaterOneTimeCheckIntervalConfiguration : UpdaterCheckIntervalConfiguration
    {
        public override TReturn Accept<TReturn>(IUpdaterCheckIntervalConfigurationVisitor<TReturn> visitor)
            => visitor.Handle(this);

        public override UpdaterManagementServiceConfigurationValidationMessage Validate()
            => UpdaterManagementServiceConfigurationValidationMessage.CreateValid();
    }
}
