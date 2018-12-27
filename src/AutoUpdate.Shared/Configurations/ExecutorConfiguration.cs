namespace AutoUpdate.Shared.Configurations
{
    public class ExecutorConfiguration
    {
        public const string DEFAULT_FILENAME = "config.xml";

        public ExecutorStepConfiguration[] Steps { get; set; }
        public ApplicationConfiguration Application { get; set; }

        public ExecutorConfiguration()
        {
            Application = new ApplicationConfiguration();
        }
    }
}
