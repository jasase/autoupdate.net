using System.Collections.Generic;
using AutoUpdate.Core.Model;
using AutoUpdate.Core.Model.Executor;

namespace AutoUpdate.Core.Abstraction
{
    public interface IUpdatePreparationStep
    {
        IEnumerable<ExecutorStepConfiguration> Prepare(UpdatePreparationWorkspaceInformation workspaceInformation);
    }
}
