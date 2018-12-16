using System.IO;
using AutoUpdate.Core.Model;

namespace AutoUpdate.Core.Abstraction
{
    public class UpdatePreparationWorkspaceInformation
    {
        public UpdatePreparationWorkspaceInformation(Version version,
                                                     DirectoryInfo workingDirectory)
        {
            Version = version;
            WorkingDirectory = workingDirectory;
        }

        public Version Version { get; }
        public DirectoryInfo WorkingDirectory { get; }
    }
}
