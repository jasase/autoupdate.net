using System.IO;
using System.IO.Compression;
using AutoUpdate.Abstraction;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public class FileDownloader : Downloader
    {
        private readonly FileVersionDownloadSource _fileVersionDownloadSource;

        public FileDownloader(FileVersionDownloadSource fileVersionDownloadSource)
        {
            _fileVersionDownloadSource = fileVersionDownloadSource;
        }

        public override void Download(UpdatePreparationWorkspaceInformation workspace)
        {
            var file = new FileInfo(_fileVersionDownloadSource.FilePath);

            if (_fileVersionDownloadSource.IsZipFile)
            {
                using (var stream = file.OpenRead())
                {
                    var archive = new ZipArchive(stream);
                    archive.ExtractToDirectory(workspace.ArtifactsDirectory.FullName);
                }
            }
            else
            {
                file.CopyTo(Path.Combine(workspace.ArtifactsDirectory.FullName, file.Name));
            }
        }
    }
}
