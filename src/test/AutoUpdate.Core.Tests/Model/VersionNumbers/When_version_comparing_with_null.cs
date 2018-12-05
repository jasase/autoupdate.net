using AutoUpdate.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Model.VersionNumbers
{
    [TestClass]
    public class When_version_comparing_with_null : SpecificationForVersionNumber
    {
        [TestMethod]
        public void Should_be_greater_than_null()
            => ShouldBeGreater(new VersionNumber(0, 0, 0, 0),
                               null);

    }
}
