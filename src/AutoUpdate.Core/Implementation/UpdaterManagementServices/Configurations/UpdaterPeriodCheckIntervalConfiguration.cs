using System;

namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public class UpdaterPeriodCheckIntervalConfiguration : UpdaterCheckIntervalConfiguration
    {
        public override TReturn Accept<TReturn>(IUpdaterCheckIntervalConfigurationVisitor<TReturn> visitor)
        {
            //TODO Implement
            var t = 0;
            throw new NotImplementedException();
        }

        public override UpdaterManagementServiceConfigurationValidationMessage Validate()
        {
            //TODO Implement
            var t = 0;
            throw new NotImplementedException();
        }
    }
}
