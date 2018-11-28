using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.VersionParsers;
using AutoUpdate.Core.Model;
using AutoUpdate.TestCore;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Tests.Parsers
{
    public abstract class SpecificationForXmlVersionParser : Specification
    {
        public abstract string XmlContent { get; }

        public IVersionParser Sut { get; private set; }
        public Version[] Result { get; private set; }

        public override void EstablishContext()
        {
            base.EstablishContext();

            Sut = new XmlVersionParser(LoggerFactory.CreateLogger<XmlVersionParser>());

        }

        public override void Because()
        {
            using (var stream = GetXmlStream())
            {
                Result = Sut.ParseVersion(stream).ToArray();
            }
        }

        private Stream GetXmlStream()
        {
            var memoryStream = new MemoryStream();

            var sw = new StreamWriter(memoryStream);
            sw.Write(XmlContent);
            sw.Flush();

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
