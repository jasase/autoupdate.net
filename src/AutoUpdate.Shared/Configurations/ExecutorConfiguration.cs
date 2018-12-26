namespace AutoUpdate.Shared.Configurations
{
    public class ExecutorConfiguration
    {
        public ExecutorStepConfiguration[] Steps { get; set; }
        public ApplicationConfiguration Application { get; set; }

        public ExecutorConfiguration()
        {
            Application = new ApplicationConfiguration();
        }
    }
}
