using AutoUpdate.Core.Model;
using AutoUpdate.Core.Model.Server;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class DownloaderFactory : IVersionDownloadSourceVisitor<Downloader>
    {
        public Downloader Handle(HttpVersionDownloadSource downloadSource)
            => new HttpDownloader(downloadSource);

        public Downloader Handle(FileVersionDownloadSource downloadSource)
            => new FileDownloader(downloadSource);

        public Downloader Handle(EmptyVersionDownloadSource downloadSource)
            => new EmptyDownloader(downloadSource);
    }
}
