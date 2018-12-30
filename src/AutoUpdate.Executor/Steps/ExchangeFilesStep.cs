using System.IO;
using AutoUpdate.Abstraction.Configurations.Steps;
using Microsoft.Extensions.Logging;

namespace AutoUpdate.Executor.Steps
{
    public class ExchangeFilesStep : ExecutorStep
    {
        private readonly ExchangeFilesStepConfiguration _configuration;

        public ExchangeFilesStep(ILoggerFactory loggerFactory, ExchangeFilesStepConfiguration configuration)
            : base(loggerFactory)
        {
            _configuration = configuration;
        }

        public override void Execute()
        {
            var destinationDirectory = new DirectoryInfo(_configuration.DestinationDirectory);
            var sourceDirectory = new DirectoryInfo(_configuration.SourceDirectory);

            foreach (var subElement in destinationDirectory.EnumerateFileSystemInfos())
            {
                subElement.Delete();
            }


            foreach (var dirPath in sourceDirectory.EnumerateDirectories("*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.FullName.Replace(sourceDirectory.FullName, destinationDirectory.FullName));
            }

            foreach (var file in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                file.CopyTo(file.FullName.Replace(sourceDirectory.FullName, destinationDirectory.FullName));
            }
        }
    }
}
