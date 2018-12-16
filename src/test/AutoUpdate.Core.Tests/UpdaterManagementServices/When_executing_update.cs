using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoUpdate.Core.Implementation.Builders;
using AutoUpdate.TestCore.Modules;
using AutoUpdate.TestMock.Assembly;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.UpdaterManagementServices
{
    [TestClass]
    public class When_executing_update : SpecificationForUpdaterManagementService
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

        public override void Because()
        {
            base.Because();

            var handle = Handles.First();
            handle.UpdateToNewVersion();
        }

        [TestMethod]
        public void Should_have_no_error()
        {
            var t = 0;
            HasThrownException.Should().BeFalse();
        }
    }

}
