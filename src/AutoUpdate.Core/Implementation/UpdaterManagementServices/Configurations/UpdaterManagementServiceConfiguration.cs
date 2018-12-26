using System.Collections.Generic;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.ApplicationClosers;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public class UpdaterManagementServiceConfiguration
    {
        public IVersionSource VersionSource { get; set; }
        public ICurrentVersionDeterminer CurrentVersionDeterminer { get; set; }
        public IApplicationCloser ApplicationCloser { get; set; }
        public UpdaterCheckIntervalConfiguration CheckInterval { get; set; }

        public List<IUserInteraction> UserInteraction { get; }
        public List<IUpdatePreparationStep> UpdatePreparationSteps { get; }

        public UpdaterManagementServiceConfiguration()
        {
            UserInteraction = new List<IUserInteraction>();
            UpdatePreparationSteps = new List<IUpdatePreparationStep>();
            ApplicationCloser = new DefaultApplicationCloser();
        }


        public UpdaterManagementServiceConfigurationValidationMessage Validate()
        {
            if (VersionSource == null)
            {
                return UpdaterManagementServiceConfigurationValidationMessage.CreateNotValid("VersionSource not set");
            }

            if (CurrentVersionDeterminer == null)
            {
                return UpdaterManagementServiceConfigurationValidationMessage.CreateNotValid("CurrentVersionDeterminer not set");
            }

            if (CheckInterval == null)
            {
                return UpdaterManagementServiceConfigurationValidationMessage.CreateNotValid("CheckInterval configuration not set");
            }

            if (ApplicationCloser == null)
            {
                return UpdaterManagementServiceConfigurationValidationMessage.CreateNotValid("Application closer not set");
            }

            var subValidate = CheckInterval.Validate();
            if (!subValidate.Valid)
            {
                return subValidate;
            }

            return UpdaterManagementServiceConfigurationValidationMessage.CreateValid();
        }
    }
}
