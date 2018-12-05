﻿using System.Collections.Generic;
using AutoUpdate.Core.Abstraction;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public class UpdaterManagementServiceConfiguration
    {
        public IVersionSource VersionSource { get; set; }
        public ICurrentVersionDeterminer CurrentVersionDeterminer { get; set; }
        public List<IUserInteraction> UserInteraction { get; }
        public UpdaterCheckIntervalConfiguration CheckInterval { get; set; }

        public UpdaterManagementServiceConfiguration()
        {
            UserInteraction = new List<IUserInteraction>();
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
            //TODO Check CheckInterval Configuration


            return UpdaterManagementServiceConfigurationValidationMessage.CreateValid();
        }
    }
}
