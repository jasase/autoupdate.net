using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Parsers
{
    [TestClass]
    public class When_parsing_little_xml_file : SpecificationForXmlVersionParser
    {
        public override string XmlResourceKey => "AutoUpdate.Core.Tests.Parsers.Example1_Little.xml";

        [TestMethod]
        public void Should_not_throw_error()
        { }
    }
}
