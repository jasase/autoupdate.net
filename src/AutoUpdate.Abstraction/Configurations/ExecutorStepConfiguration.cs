using System.Xml.Serialization;
using AutoUpdate.Abstraction.Configurations.Steps;

namespace AutoUpdate.Abstraction.Configurations
{
    [XmlInclude(typeof(ExchangeFilesStepConfiguration))]
    public abstract class ExecutorStepConfiguration
    {
        public abstract TReturn Accept<TReturn>(IExecutorStepConfigurationVisitor<TReturn> visitor);

        public abstract void Accept(IExecutorStepConfigurationVisitor visitor);
    }
}

