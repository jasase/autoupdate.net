using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class EmptyDownloader : Downloader
    {
        private EmptyVersionDownloadSource _downloadSource;

        public EmptyDownloader(EmptyVersionDownloadSource downloadSource)
        {
            _downloadSource = downloadSource;
        }

        public override void Download(UpdatePreparationWorkspaceInformation workspace)
        { }
    }
}
