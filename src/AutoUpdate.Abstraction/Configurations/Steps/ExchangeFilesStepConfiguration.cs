namespace AutoUpdate.Abstraction.Configurations.Steps
{
    public class ExchangeFilesStepConfiguration : ExecutorStepConfiguration
    {
        public string DestinationDirectory { get; set; }
        public string SourceDirectory { get; set; }

        public override TReturn Accept<TReturn>(IExecutorStepConfigurationVisitor<TReturn> visitor)
            => visitor.Handle(this);

        public override void Accept(IExecutorStepConfigurationVisitor visitor)
            => visitor.Handle(this);
    }
}
