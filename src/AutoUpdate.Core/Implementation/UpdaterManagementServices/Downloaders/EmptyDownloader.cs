using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class EmptyDownloader : Downloader
    {
        private EmptyVersionDownloadSource _downloadSource;

        public EmptyDownloader(ILoggerFactory loggerFactory, EmptyVersionDownloadSource downloadSource)
            : base(loggerFactory)
        {
            _downloadSource = downloadSource;
        }

        public override void Download(UpdatePreparationWorkspaceInformation workspace)
        { }
    }
}
