using AutoUpdate.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Model.VersionNumbers
{
    [TestClass]
    public class When_versions_are_equals : SpecificationForVersionNumber
    {
        [TestMethod]
        public void Should_be_equal_1()
            => ShouldBeEqual(new VersionNumber(1, 1, 1, 1),
                             new VersionNumber(1, 1, 1, 1));

        [TestMethod]
        public void Should_be_equal_2()
            => ShouldBeEqual(new VersionNumber(1, 2, 3, 4),
                             new VersionNumber(1, 2, 3, 4));

        [TestMethod]
        public void Should_be_equal_3()
            => ShouldBeEqual(new VersionNumber(0, 0, 0, 0),
                             new VersionNumber(0, 0, 0, 0));

        [TestMethod]
        public void Should_be_equal_4()
            => ShouldBeEqual(new VersionNumber(),
                             new VersionNumber());

        [TestMethod]
        public void Should_be_equal_null()
            => ShouldBeEqual(null,
                             null);
    }
}
