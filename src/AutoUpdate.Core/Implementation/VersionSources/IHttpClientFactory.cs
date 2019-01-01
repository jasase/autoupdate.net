using System.Net.Http;

namespace AutoUpdate.Core.Implementation.VersionSources
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();         
    }
}
