using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Parsers
{
    [TestClass]
    public class When_parsing_xml_with_unknown_source_type : SpecificationForXmlVersionParser
    {
        [TestMethod]
        public void Should_have_no_result()
        {
            Result.Should().NotBeNull();
            Result.Should().HaveCount(0);
        }

        public override string XmlContent => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>1.5.2.3</VersionNumber>
    <ChangeLog>Example change log</ChangeLog>
    <Mandatory>false</Mandatory>
    <SourceType>Http23</SourceType>
    <SourcePath>http://google.de</SourcePath>
  </Version>
</ParserVersionDefinition>";
    }
}
