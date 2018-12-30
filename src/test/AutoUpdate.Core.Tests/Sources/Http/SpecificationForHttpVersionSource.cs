using System.Net.Http;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;
using AutoUpdate.Core.Implementation.VersionParsers;
using AutoUpdate.Core.Implementation.VersionSources;
using AutoUpdate.TestCore;
using AutoUpdate.TestCore.Modules;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Tests.Sources.Http
{
    public abstract class SpecificationForHttpVersionSource : Specification
    {
        public IVersionSource Sut { get; private set; }
        public Version[] Result { get; private set; }

        public override void EstablishContext()
        {
            base.EstablishContext();

            var module = Module<HttpServerTestModule>();
            var serverContent = ProviderServerContent();
            if (serverContent != null)
            {
                module.AddContent("/version", serverContent);
            }

            var parser = new XmlVersionParser(LoggerFactory);
            Sut = new HttpVersionSource(LoggerFactory, parser, Module<HttpServerTestModule>());
        }

        public abstract string ProviderServerContent();

        public override void Because()
            => Result = Sut.LoadAvailableVersions();
    }
}
