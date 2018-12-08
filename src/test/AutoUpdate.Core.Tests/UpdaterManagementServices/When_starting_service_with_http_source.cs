using System.Linq;
using AutoUpdate.Core.Implementation.Builders;
using AutoUpdate.Core.Model;
using AutoUpdate.TestCore.Modules;
using AutoUpdate.TestMock.Assembly;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.UpdaterManagementServices
{
    [TestClass]
    public class When_starting_service_with_http_source : SpecificationForUpdaterManagementService
    {
        public override string ProviderServerContent()
           => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>4.22.1.6</VersionNumber>
    <ChangeLog>Example change log</ChangeLog>
    <Mandatory>false</Mandatory>
    <SourceType>Http</SourceType>
    <SourcePath>http://google.de</SourcePath>
  </Version>
</ParserVersionDefinition>";

        protected override void ConfigureBuilder(UpdateBuilder builder)
            => builder.ConfigureOneTimeCheck()
                      .UseUserInteraction(this)
                      .UseHttpSource(Module<HttpServerTestModule>()).UseDefaultXmlParser()
                      .UseAssemblyCurrentVersionDeterminer<MockTestClass>();

        [TestMethod]
        public void Should_have_one_version()
        {
            Handles.Should().HaveCount(1);

            var first = Handles.First();
            first.HasNewVersion.Should().BeTrue();

            var firsVersion = first.NewVersion;

            firsVersion.ChangeLog.Should().Be("Example change log");
            firsVersion.Mandatory.Should().Be(false);
            
            firsVersion.VersionNumber.Should().NotBeNull();
            firsVersion.VersionNumber.Major.Should().Be(4);
            firsVersion.VersionNumber.Minor.Should().Be(22);
            firsVersion.VersionNumber.Build.Should().Be(1);
            firsVersion.VersionNumber.Revision.Should().Be(6);
            
            firsVersion.Source.Should().NotBeNull();
            firsVersion.Source.Should().BeOfType<HttpVersionDownloadSource>();
            ((HttpVersionDownloadSource) firsVersion.Source).Url.Should().Be("http://google.de");
        }
    }
}
