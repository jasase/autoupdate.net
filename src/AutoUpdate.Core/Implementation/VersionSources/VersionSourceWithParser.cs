using System;
using System.IO;
using System.Linq;
using AutoUpdate.Abstraction;
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

        public Abstraction.Model.Version[] LoadAvailableVersions()
        {
            Stream data = null;
            var result = new Abstraction.Model.Version[0];
            try
            {
                data = GetContentStream();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during requesting version information from source");
            }

            if (data != null)
            {
                try
                {
                    result = _parser.ParseVersion(data).ToArray();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during parsing version information");
                }
            }
            return result;
        }

        protected abstract Stream GetContentStream();
    }
}
