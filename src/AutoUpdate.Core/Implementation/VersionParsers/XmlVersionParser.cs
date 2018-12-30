using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.VersionParsers
{
    public class XmlVersionParser : IVersionParser
    {
        public const string XML_SOURCE_TYPE_HTTP = "HTTP";
        public const string XML_SOURCE_TYPE_FILE = "FILE";

        private readonly XmlSerializer _serializer;
        private readonly Regex _versionNumberRegex;
        private readonly ILogger _logger;

        public XmlVersionParser(ILoggerFactory loggerFactory)
        {
            _serializer = new XmlSerializer(typeof(ParserVersionDefinition));
            _versionNumberRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)$");
            _logger = loggerFactory.CreateLogger<XmlVersionParser>();
        }

        public IEnumerable<Version> ParseVersion(Stream content)
        {
            var result = _serializer.Deserialize(content) as ParserVersionDefinition;
            if (result == null || result.Version == null)
            {
                _logger.LogWarning("Reading of XML not successful. XML empty or does not contain any version");
                yield break;
            }

            var counter = 0;
            foreach (var cur in result.Version)
            {
                var versionNumberMatch = _versionNumberRegex.Match(cur.VersionNumber);
                if (!versionNumberMatch.Success)
                {
                    _logger.LogWarning("Ignoring version in XML at position {0} because version number couldn't be parsed: '{1}'",
                                       counter,
                                       cur.VersionNumber);
                    continue;
                }
                var versionNumber = new VersionNumber(int.Parse(versionNumberMatch.Groups[1].Value),
                                                      int.Parse(versionNumberMatch.Groups[2].Value),
                                                      int.Parse(versionNumberMatch.Groups[3].Value),
                                                      int.Parse(versionNumberMatch.Groups[4].Value));

                var source = DetermineSource(cur);
                if (source == null)
                {
                    _logger.LogWarning("Ignoring version in XML at position {0} because version source couldn't be determinded: '{1}'",
                                       counter,
                                       cur.SourceType);
                    continue;
                }

                yield return new Version
                {
                    ChangeLog = cur.ChangeLog,
                    Mandatory = cur.Mandatory,
                    Source = source,
                    VersionNumber = versionNumber
                };
                counter++;
            }
        }

        private VersionDownloadSource DetermineSource(ParserVersion cur)
        {
            var sourceType = (cur.SourceType ?? string.Empty).ToUpperInvariant();

            if (sourceType == XML_SOURCE_TYPE_HTTP)
            {
                return new HttpVersionDownloadSource
                {
                    Url = cur.SourcePath
                };
            }
            else if (sourceType == XML_SOURCE_TYPE_FILE)
            {
                return new FileVersionDownloadSource
                {
                    FilePath = cur.SourcePath
                };
            }
            else
            {
                return null;
            }
        }
    }
}
