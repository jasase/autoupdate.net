using System.IO;
using System.IO.Compression;
using System.Net;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class HttpDownloader : Downloader
    {
        private readonly HttpVersionDownloadSource _downloadSource;

        public HttpDownloader(HttpVersionDownloadSource downloadSource)
        {
            _downloadSource = downloadSource;
        }

        public override void Download(UpdatePreparationWorkspaceInformation workspace)
        {
            var request = WebRequest.CreateHttp(_downloadSource.Url);
            var response = request.GetResponse();
            var content = response.GetResponseStream();

            if (_downloadSource.IsZipFile)
            {
                var archive = new ZipArchive(content);
                archive.ExtractToDirectory(workspace.ArtifactsDirectory.FullName);
            }
            else
            {
                var destFile = Path.Combine(workspace.ArtifactsDirectory.FullName, _downloadSource.FileName);
                using (var stream = File.OpenWrite(destFile))
                {
                    content.CopyTo(stream);
                }
            }
        }
    }
}
