﻿using System.Linq;
using AutoUpdate.Abstraction.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Parsers.Xml
{
    [TestClass]
    public class When_parsing_xml_with_one_version : SpecificationForXmlVersionParser
    {

        [TestMethod]
        public void Should_have_correct_result()
        {
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
            ((HttpVersionDownloadSource) first.Source).Url.Should().Be("http://google.de/file.xml");
            ((HttpVersionDownloadSource) first.Source).FileName.Should().Be("file.xml");
            ((HttpVersionDownloadSource) first.Source).IsZipFile.Should().Be(false);
        }

        public override string XmlContent => @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ParserVersionDefinition>
  <Version>
    <VersionNumber>2.3.4.5</VersionNumber>
    <ChangeLog>Example change log</ChangeLog>
    <Mandatory>false</Mandatory>
    <SourceType>Http</SourceType>
    <SourcePath>http://google.de/file.xml</SourcePath>
  </Version>
</ParserVersionDefinition>";

    }
}
