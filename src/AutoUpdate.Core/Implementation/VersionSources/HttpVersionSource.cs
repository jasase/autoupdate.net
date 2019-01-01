using System.IO;
using AutoUpdate.Abstraction;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public class HttpVersionSource : VersionSourceWithParser
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpVersionSource(ILoggerFactory loggerFactory,
                                 IVersionParser parser,
                                 IHttpClientFactory httpClientFactory)
            : base(loggerFactory, parser)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override Stream GetContentStream()
        {
            var client = _httpClientFactory.CreateClient();

            var t = client.GetAsync("").Result;
            //var t = client.GetAsync("/version").Result;

            return t.Content.ReadAsStreamAsync().Result;
        }
    }
}
