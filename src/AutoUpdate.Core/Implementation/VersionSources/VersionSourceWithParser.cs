using System.IO;
using System.Linq;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Model;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public abstract class VersionSourceWithParser : IVersionSource
    {
        protected readonly ILogger _logger;
        private readonly IVersionParser _parser;

        public VersionSourceWithParser(ILoggerFactory loggerFactory,
                                       IVersionParser parser)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _parser = parser;
        }

        public Version[] LoadAvailableVersions()
        {
            //TODO: Error handling
            var s = GetContentStream();
            return _parser.ParseVersion(s).ToArray();
        }

        protected abstract Stream GetContentStream();
    }
}
