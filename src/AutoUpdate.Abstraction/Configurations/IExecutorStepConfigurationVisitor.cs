using AutoUpdate.Abstraction.Configurations.Steps;

namespace AutoUpdate.Abstraction.Configurations
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
