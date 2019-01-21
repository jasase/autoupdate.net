using System.IO;
using System.IO.Compression;
using System.Net;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class HttpDownloader : Downloader
    {
        private readonly HttpVersionDownloadSource _downloadSource;

        public HttpDownloader(ILoggerFactory loggerFactory, HttpVersionDownloadSource downloadSource)
            : base(loggerFactory)
        {
            _downloadSource = downloadSource;
        }

        public override void Download(UpdatePreparationWorkspaceInformation workspace)
        {
            Logger.LogDebug("Starting download from '{0}'", _downloadSource.Url);
            var request = WebRequest.CreateHttp(_downloadSource.Url);
            var response = request.GetResponse();
            var content = response.GetResponseStream();

            if (_downloadSource.IsZipFile)
            {
                Logger.LogDebug("Extracting content of zip file to '{0}'", workspace.ArtifactsDirectory.FullName);
                var archive = new ZipArchive(content);
                archive.ExtractToDirectory(workspace.ArtifactsDirectory.FullName);
            }
            else
            {
                var destFile = Path.Combine(workspace.ArtifactsDirectory.FullName, _downloadSource.FileName);
                using (var stream = File.OpenWrite(destFile))
                {
                    Logger.LogDebug("Saving content at '{0}'", destFile);
                    content.CopyTo(stream);
                }
            }
        }
    }
}
