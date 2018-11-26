using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.VersionParsers;
using AutoUpdate.TestCore;

namespace AutoUpdate.Core.Tests.Parsers
{
    public abstract class SpecificationForXmlVersionParser : Specification
    {
        public abstract string XmlResourceKey { get; }

        public IVersionParser Sut { get; private set; }
        public IEnumerable<Version> Result { get; private set; }

        public override void EstablishContext()
        {
            base.EstablishContext();

            Sut = new XmlVersionParser();

        }

        public override void Because()
        {
            var stream = GetXmlStream();
            Result = Sut.ParseVersion(stream).ToArray();
        }

        private Stream GetXmlStream()
        {
            var t = GetType().Assembly.GetManifestResourceNames();
            var stream = GetType().Assembly.GetManifestResourceStream(XmlResourceKey);
            return stream;
        }
    }
}
