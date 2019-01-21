using AutoUpdate.Abstraction.Model;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class DownloaderFactory : IVersionDownloadSourceVisitor<Downloader>
    {
        private readonly ILoggerFactory _loggerFactory;

        public DownloaderFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Downloader Handle(HttpVersionDownloadSource downloadSource)
            => new HttpDownloader(_loggerFactory, downloadSource);

        public Downloader Handle(FileVersionDownloadSource downloadSource)
            => new FileDownloader(_loggerFactory, downloadSource);

        public Downloader Handle(EmptyVersionDownloadSource downloadSource)
            => new EmptyDownloader(_loggerFactory, downloadSource);
    }
}
