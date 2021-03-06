﻿using System.IO;
using AutoUpdate.Abstraction.Model;

namespace AutoUpdate.Abstraction
{
    public class UpdatePreparationWorkspaceInformation
    {
        public UpdatePreparationWorkspaceInformation(Version version,
                                                     DirectoryInfo workingDirectory,
                                                     DirectoryInfo artifactsDirectory)
        {
            Version = version;
            WorkingDirectory = workingDirectory;
            ArtifactsDirectory = artifactsDirectory;
        }

        public Version Version { get; }
        public DirectoryInfo WorkingDirectory { get; }
        public DirectoryInfo ArtifactsDirectory { get; }
    }
}
