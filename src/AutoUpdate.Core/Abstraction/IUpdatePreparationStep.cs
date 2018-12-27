using System.Collections.Generic;
using AutoUpdate.Shared.Configurations;

namespace AutoUpdate.Core.Abstraction
{
    public interface IUpdatePreparationStep
    {
        IEnumerable<ExecutorStepConfiguration> Prepare(UpdatePreparationWorkspaceInformation workspaceInformation);
    }
}
