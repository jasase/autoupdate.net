using AutoUpdate.Core.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Parsers.Xml
{
    [TestClass]
    public class When_parsing_xml_with_multi_version : SpecificationForXmlVersionParser
    {
        [TestMethod]
        public void Should_have_correct_result()
        {
            Result.Should().HaveCount(2);

            var first = Result[0];

            first.ChangeLog.Should().Be("Example change log");
            first.Mandatory.Should().Be(false);

            first.VersionNumber.Should().NotBeNull();
            first.VersionNumber.Major.Should().Be(2);
            first.VersionNumber.Minor.Should().Be(3);
            first.VersionNumber.Build.Should().Be(4);
            first.VersionNumber.Revision.Should().Be(5);

            first.Source.Should().NotBeNull();
            first.Source.Should().BeOfType<HttpVersionDownloadSource>();
            ((HttpVersionDownloadSource) first.Source).Url.Should().Be("http://google.de");

            var second = Result[1];
            second.ChangeLog.Should().Be("Example change log2");
            second.Mandatory.Should().Be(true);

            second.VersionNumber.Should().NotBeNull();
            second.VersionNumber.Major.Should().Be(6);
            second.VersionNumber.Minor.Should().Be(7);
            second.VersionNumber.Build.Should().Be(8);
            second.VersionNumber.Revision.Should().Be(9);

            second.Source.Should().NotBeNull();
            second.Source.Should().BeOfType<FileVersionDownloadSource>();
            ((FileVersionDownloadSource) second.Source).FilePath.Should().Be(@"C:\File\test.txt");
        }

        public override string XmlContent => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>2.3.4.5</VersionNumber>
    <ChangeLog>Example change log</ChangeLog>
    <Mandatory>false</Mandatory>
    <SourceType>Http</SourceType>
    <SourcePath>http://google.de</SourcePath>
  </Version>
  <Version>
    <VersionNumber>6.7.8.9</VersionNumber>
    <ChangeLog>Example change log2</ChangeLog>
    <Mandatory>true</Mandatory>
    <SourceType>File</SourceType>
    <SourcePath>C:\File\test.txt</SourcePath>
  </Version>
</ParserVersionDefinition>";

    }
}
