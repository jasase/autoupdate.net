using AutoUpdate.Abstraction.Model;
using AutoUpdate.TestCore;
using FluentAssertions;

namespace AutoUpdate.Core.Tests.Model.VersionNumbers
{
    public class SpecificationForVersionNumber : Specification
    {
        /// <summary>
        /// a > b
        /// </summary>
        public void ShouldBeGreater(VersionNumber a, VersionNumber b)
        {
            (a > b).Should().BeTrue();
            (b < a).Should().BeTrue();

            (a < b).Should().BeFalse();
            (b > a).Should().BeFalse();

            (a != b).Should().BeTrue();
            (a == b).Should().BeFalse();

            a.CompareTo(b).Should().BeGreaterThan(0);
            a.Equals(b).Should().BeFalse();

            if (b != null)
            {
                b.CompareTo(a).Should().BeLessThan(0);
                b.Equals(a).Should().BeFalse();
            }
        }

        public void ShouldBeEqual(VersionNumber a, VersionNumber b)
        {
            (a > b).Should().BeFalse();
            (b < a).Should().BeFalse();

            (a < b).Should().BeFalse();
            (b > a).Should().BeFalse();

            (a != b).Should().BeTrue();
            (a == b).Should().BeFalse();

            a.CompareTo(b).Should().Be(0);
            a.Equals(b).Should().BeTrue();

            if (b != null)
            {
                b.CompareTo(a).Should().Be(0);
                b.Equals(a).Should().BeTrue();
            }
        }
    }
}
