namespace AutoUpdate.Shared.Configurations
{
    public class ApplicationConfiguration
    {
        public string Path { get; set; }
        public string RestartArguments { get; set; }

        public int CallingProcessId { get; set; }
    }
}
