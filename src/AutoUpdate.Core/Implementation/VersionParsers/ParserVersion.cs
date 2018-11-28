using System;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdate.Core.Implementation.VersionParsers
{
    public class ParserVersion
    {
        public string VersionNumber { get; set; }
        public string ChangeLog { get; set; }
        public bool Mandatory { get; set; }
        public string SourceType { get; set; }
        public string SourcePath { get; set; }
    }

    public class ParserVersionDefinition
    {
        [System.Xml.Serialization.XmlElementAttribute("Version")]
        public ParserVersion[] Version { get; set; }
    }
}
