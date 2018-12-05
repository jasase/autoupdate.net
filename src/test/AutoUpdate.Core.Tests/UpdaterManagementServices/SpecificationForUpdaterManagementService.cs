using System.Collections.Generic;
using System.Net.Http;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Core.Implementation.Builders;
using AutoUpdate.Core.Implementation.UpdaterManagementService;
using AutoUpdate.TestCore;
using AutoUpdate.TestCore.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.Core.Tests.UpdaterManagementServices
{
    [TestClass]
    public abstract class SpecificationForUpdaterManagementService : Specification, IUserInteraction
    {
        public IUpdaterManagementService Sut { get; private set; }
        public List<IUpdateVersionHandle> Handles { get; private set; }

        public HttpClient CreateClient()
            => Module<HttpServerTestModule>().HttpClient;

        public override void EstablishContext()
        {
            base.EstablishContext();

            Handles = new List<IUpdateVersionHandle>();

            var module = Module<HttpServerTestModule>();
            var serverContent = ProviderServerContent();
            if (serverContent != null)
            {
                module.AddContent("/version", serverContent);
            }

            var builder = new UpdateBuilder();
            ConfigureBuilder(builder);
            Sut = builder.Build();
        }

        public override void Because()
            => Sut.Start();

        public void NewVersionAvailable(IUpdateVersionHandle handle)
            => Handles.Add(handle);

        protected abstract void ConfigureBuilder(UpdateBuilder builder);

        public abstract string ProviderServerContent();
    }
}
