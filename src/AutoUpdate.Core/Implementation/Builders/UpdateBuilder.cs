using System;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.UpdaterManagementServices;
using AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.Builders
{
    public class UpdateBuilder
    {
        private UpdaterManagementServiceConfiguration _configuration;
        internal ILoggerFactory LoggerFactory { get; }

        public UpdateBuilder()
            : this(new LoggerFactory())
        { }

        public UpdateBuilder(ILoggerFactory loggerFactory)
        {
            _configuration = new UpdaterManagementServiceConfiguration();
            LoggerFactory = loggerFactory;
        }

        public UpdateBuilder ConfigureOneTimeCheck()
        {
            _configuration.CheckInterval = new UpdaterOneTimeCheckIntervalConfiguration();
            return this;
        }

        public UpdateBuilder ConfigureManualCheck()
        {
            _configuration.CheckInterval = new UpdaterManualCheckIntervalConfiguration();
            return this;
        }

        //public UpdateBuilder ConfigureIntervalCheck()
        //{
        //    _configuration.CheckInterval = new UpdaterPeriodCheckIntervalConfiguration();
        //    return this;
        //}

        public UpdateBuilder UseSource(IVersionSource source)
        {
            _configuration.VersionSource = source;
            return this;
        }

        public UpdateBuilder UseCurrentVersionDetermine(ICurrentVersionDeterminer currentVersionDeterminer)
        {
            _configuration.CurrentVersionDeterminer = currentVersionDeterminer;
            return this;
        }

        public UpdateBuilder UseUserInteraction(IUserInteraction userInteractionHandle)
        {
            _configuration.UserInteraction.Add(userInteractionHandle);
            return this;
        }

        public IUpdaterManagementService Build()
        {
            var validateMessage = _configuration.Validate();
            if (!validateMessage.Valid)
            {
                throw new InvalidOperationException("Configuration is not valid: " + validateMessage.Error);
            }
            return new UpdaterManagementService(LoggerFactory, _configuration);
        }
    }
}
