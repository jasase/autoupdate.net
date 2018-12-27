using System.Collections.Generic;
using AutoUpdate.Core.Abstraction;
using AutoUpdate.Shared.Configurations;
using AutoUpdate.Shared.Configurations.Steps;

namespace AutoUpdate.Core.Implementation.UpdatePreparationSteps
{
    public class SimpleReplacePrepareStep : IUpdatePreparationStep
    {
        private readonly string _applicationReplaceDirectory;

        public SimpleReplacePrepareStep(string applicationReplaceDirectory)
        {
            _applicationReplaceDirectory = applicationReplaceDirectory;
        }

        public IEnumerable<ExecutorStepConfiguration> Prepare(UpdatePreparationWorkspaceInformation workspaceInformation)
        {
            yield return new ExchangeFilesStepConfiguration()
            {
                DestinationDirectory = _applicationReplaceDirectory,
                SourceDirectory = workspaceInformation.ArtifactsDirectory.FullName
            };
        }
    }
}
