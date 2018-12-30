using AutoUpdate.Abstraction;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public abstract class Downloader
    {
        public abstract void Download(UpdatePreparationWorkspaceInformation workspace);
    }
}
