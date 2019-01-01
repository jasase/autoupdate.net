using System;
using System.Net.Http;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly Uri _uri;

        public DefaultHttpClientFactory(Uri uri)
        {
            _uri = uri;
        }

        public HttpClient CreateClient()
        {
            var handler = new HttpClient();
            handler.BaseAddress = _uri;
            return handler;
        }
    }
}
