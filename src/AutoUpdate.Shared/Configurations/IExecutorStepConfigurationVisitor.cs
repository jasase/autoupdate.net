using AutoUpdate.Shared.Configurations.Steps;

namespace AutoUpdate.Shared.Configurations
{
    public interface IExecutorStepConfigurationVisitor
    {
        void Handle(ExchangeFilesStepConfiguration configuration);
    }

    public interface IExecutorStepConfigurationVisitor<TReturn>
    {
        TReturn Handle(ExchangeFilesStepConfiguration configuration);
    }
}
