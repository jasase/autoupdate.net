using System;
using System.Collections.Generic;
using System.Text;
using AutoUpdate.Core.Implementation.CurrentVersionDeterminers;
using AutoUpdate.Core.Model;
using AutoUpdate.TestCore;
using AutoUpdate.TestMock.Assembly;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.CurrentVersionDeterminers
{
    [TestClass]
    public class When_getting_current_version : Specification
    {
        public AssemblyCurrentVersionDeterminer Sut { get; private set; }
        public VersionNumber Result { get; private set; }

        public override void EstablishContext()
        {
            base.EstablishContext();

            Sut = new AssemblyCurrentVersionDeterminer(typeof(MockTestClass).Assembly);
        }

        public override void Because()
            => Result = Sut.DetermineCurrentVersionNumber();

        [TestMethod]
        public void Should_get_correct_version()
        {
            Result.Major.Should().Be(4);
            Result.Minor.Should().Be(22);
            Result.Build.Should().Be(1);
            Result.Revision.Should().Be(5);
        }
    }
}
