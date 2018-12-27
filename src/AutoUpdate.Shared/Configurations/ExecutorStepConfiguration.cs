using System.Xml.Serialization;
using AutoUpdate.Shared.Configurations.Steps;

namespace AutoUpdate.Shared.Configurations
{
    [XmlInclude(typeof(ExchangeFilesStepConfiguration))]
    public abstract class ExecutorStepConfiguration
    {
        public abstract TReturn Accept<TReturn>(IExecutorStepConfigurationVisitor<TReturn> visitor);

        public abstract void Accept(IExecutorStepConfigurationVisitor visitor);
    }
}

