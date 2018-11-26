using System;
using System.Collections.Generic;
using System.IO;
using AutoUpdate.Core.Abstraction;

namespace AutoUpdate.Core.Implementation.VersionParsers
{
    public class XmlVersionParser : IVersionParser
    {
        public IEnumerable<Version> ParseVersion(Stream content)
        {
            throw new NotImplementedException();
        }
    }
}
