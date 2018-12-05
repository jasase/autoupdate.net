using System;
using System.Collections.Generic;
using System.Text;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.UpdaterManagementServices;

namespace AutoUpdate.Core.Implementation.Builders
{
    public class UpdateBuilder
    {
        public UpdateBuilder ConfigureOneTimeCheck()
        {
            var t = 0;
            return this;
        }

        public UpdateBuilder ConfigureIntervalCheck()
        {
            var t = 0;
            return this;
        }

        public UpdateBuilder UseSource(IVersionSource source)
        {
            var t = 0;
            return this;
        }

        public UpdateBuilder UseCurrentVersionDetermine(ICurrentVersionDeterminer currentVersionDeterminer)
        {
            var t = 0;
            return this;
        }

        public UpdateBuilder UseUserInteraction(IUserInteraction userInteractionHandle)
        {
            var t = 0;
            return this;
        }

        public IUpdaterManagementService Build()
            => new UpdaterManagementService();
    }
}
