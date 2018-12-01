using System.Linq;
using AutoUpdate.Core.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Sources.Http
{
    [TestClass]
    public class When_loading_simple_version : SpecificationForHttpVersionSource
    {
        public override string ProviderServerContent()
            => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>2.3.4.5</VersionNumber>
    <ChangeLog>Example change log</ChangeLog>
    <Mandatory>false</Mandatory>
    <SourceType>Http</SourceType>
    <SourcePath>http://google.de</SourcePath>
  </Version>
</ParserVersionDefinition>";

        [TestMethod]
        public void Should_recieve_one_version()
        {
            Result.Should().NotBeNull();
            Result.Should().HaveCount(1);

            var first = Result.First();

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
        }
    }
}
