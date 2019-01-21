using AutoUpdate.Abstraction;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Core.Implementation.Downloaders
{
    public abstract class Downloader
    {
        protected ILogger Logger { get; }

        public Downloader(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        public abstract void Download(UpdatePreparationWorkspaceInformation workspace);
    }
}
