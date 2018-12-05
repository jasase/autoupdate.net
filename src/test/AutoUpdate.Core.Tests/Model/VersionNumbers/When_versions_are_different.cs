using AutoUpdate.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Model.VersionNumbers
{
    [TestClass]
    public class When_versions_are_different : SpecificationForVersionNumber
    {
        [TestMethod]
        public void Should_a_be_greater_than_b_major_1()
            => ShouldBeGreater(new VersionNumber(2, 0, 0, 0),
                               new VersionNumber(1, 60, 323, 1));

        [TestMethod]
        public void Should_a_be_greater_than_b_major_2()
            => ShouldBeGreater(new VersionNumber(1, 0, 0, 0),
                               new VersionNumber(0, 0, 0, 0));

        [TestMethod]
        public void Should_a_be_greater_than_b_minor_1()
            => ShouldBeGreater(new VersionNumber(1, 2, 0, 0),
                               new VersionNumber(1, 1, 323, 1));

        [TestMethod]
        public void Should_a_be_greater_than_b_minor_2()
            => ShouldBeGreater(new VersionNumber(0, 1, 0, 0),
                               new VersionNumber(0, 0, 0, 0));

        [TestMethod]
        public void Should_a_be_greater_than_b_build_1()
            => ShouldBeGreater(new VersionNumber(1, 2, 2, 0),
                               new VersionNumber(1, 2, 1, 1));

        [TestMethod]
        public void Should_a_be_greater_than_b_build_2()
            => ShouldBeGreater(new VersionNumber(0, 0, 1, 0),
                               new VersionNumber(0, 0, 0, 0));

        [TestMethod]
        public void Should_a_be_greater_than_b_revision_1()
            => ShouldBeGreater(new VersionNumber(1, 2, 3, 2),
                               new VersionNumber(1, 2, 3, 1));

        [TestMethod]
        public void Should_a_be_greater_than_b_revision_2()
            => ShouldBeGreater(new VersionNumber(0, 0, 0, 1),
                               new VersionNumber(0, 0, 0, 0));
    }
}
