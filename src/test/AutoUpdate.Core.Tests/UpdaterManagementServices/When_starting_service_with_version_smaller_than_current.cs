using AutoUpdate.Core.Implementation.Builders;
using AutoUpdate.TestCore.Modules;
using AutoUpdate.TestMock.Assembly;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.UpdaterManagementServices
{
    [TestClass]
    public class When_starting_service_with_version_smaller_than_current : SpecificationForUpdaterManagementService
    {
        public override string ProviderServerContent()
           => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>4.22.1.4</VersionNumber>
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
        public void Should_have_no_handle()
            => Handles.Should().HaveCount(0);
    }
}
