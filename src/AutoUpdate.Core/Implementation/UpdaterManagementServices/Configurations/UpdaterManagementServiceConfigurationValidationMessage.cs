namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public class UpdaterManagementServiceConfigurationValidationMessage
    {
        private UpdaterManagementServiceConfigurationValidationMessage(bool valid, string error)
        {
            Valid = valid;
            Error = error ?? string.Empty;
        }

        public bool Valid { get; }
        public string Error { get; }

        public static UpdaterManagementServiceConfigurationValidationMessage CreateValid()
            => new UpdaterManagementServiceConfigurationValidationMessage(true, string.Empty);

        public static UpdaterManagementServiceConfigurationValidationMessage CreateNotValid(string error)
            => new UpdaterManagementServiceConfigurationValidationMessage(false, error);
    }
}
