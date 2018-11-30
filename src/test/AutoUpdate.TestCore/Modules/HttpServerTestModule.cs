using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace AutoUpdate.TestCore.Modules
{
    public class HttpServerTestModule : ITestModule
    {
        private TestServer _server;
        private Dictionary<string, ServerContent> _content;

        public HttpClient HttpClient { get; private set; }

        public void Setup(Specification specification)
        {
            _content = new Dictionary<string, ServerContent>();

            var builder = new WebHostBuilder()
                .Configure(x =>
               {
                   x.UseStaticFiles(new StaticFileOptions
                   {
                       FileProvider = new TestFileProvider(this),
                       ServeUnknownFileTypes = true,
                       //RequestPath = "/root"
                   });
               });

            _server = new TestServer(builder);
            HttpClient = _server.CreateClient();
        }

        public void CleanUp()
        {
            HttpClient.Dispose();
            _server.Dispose();
        }

        public void AddContent(string path, string content)
        {
            var contentDto = new ServerContent(path, content);
            _content[contentDto.Path] = contentDto;
        }

        class ServerContent : IFileInfo
        {
            private readonly byte[] _byteContent;

            public ServerContent(string path, string content)
            {
                Path = path;
                Content = content;
                _byteContent = Encoding.Default.GetBytes(content);
            }

            public string Path { get; }
            public string Content { get; }
            public bool Exists => true;
            public long Length => _byteContent.Length;
            public string PhysicalPath => string.Empty;
            public string Name => string.Empty;
            public DateTimeOffset LastModified => DateTime.Now;
            public bool IsDirectory => false;

            public Stream CreateReadStream()
                => new MemoryStream(_byteContent);
        }

        class TestFileProvider : IFileProvider
        {
            private readonly HttpServerTestModule _module;

            public TestFileProvider(HttpServerTestModule module)
            {
                _module = module;
            }

            public IDirectoryContents GetDirectoryContents(string subpath)
                => new NotFoundDirectoryContents();

            public IFileInfo GetFileInfo(string subpath)
            {
                if (_module._content.ContainsKey(subpath))
                {
                    return _module._content[subpath];
                }

                return new NotFoundFileInfo(subpath);
            }

            public IChangeToken Watch(string filter)
                => NullChangeToken.Singleton;
        }
    }
}
