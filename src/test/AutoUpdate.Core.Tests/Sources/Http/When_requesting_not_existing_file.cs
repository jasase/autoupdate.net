using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.Sources.Http
{
    [TestClass]
    public class When_requesting_not_existing_file : SpecificationForHttpVersionSource
    {
        public override bool RaiseExceptionsInBecause
            => false;

        public override string ProviderServerContent()
            => null;

        [TestMethod]
        public void Should_create_empty_result()
            => Result.Should().HaveCount(0);

        [TestMethod]
        public void Should_not_have_an_error()
            => HasThrownException.Should().BeFalse();
    }
}
