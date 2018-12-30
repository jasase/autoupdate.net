using System.Collections.Generic;
using AutoUpdate.Abstraction.Configurations;

namespace AutoUpdate.Abstraction
{
    public interface IUpdatePreparationStep
    {
        IEnumerable<ExecutorStepConfiguration> Prepare(UpdatePreparationWorkspaceInformation workspaceInformation);
    }
}
