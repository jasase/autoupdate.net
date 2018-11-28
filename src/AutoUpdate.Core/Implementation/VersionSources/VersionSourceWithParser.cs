using System;
using AutoUpdate.Core.Abstraction;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public abstract class VersionSourceWithParser : IVersionSource
    {
        private readonly IVersionParser _parser;

        public VersionSourceWithParser(IVersionParser parser)
        {
            _parser = parser;
        }

        public Version[] LoadAvailableVersions()
        {
            throw new NotImplementedException();
        }
    }
}
