namespace AutoUpdate.Core.Model
{
    public abstract class VersionSource
    {
    }

    public class HttpVersionSource : VersionSource
    {
        public string Url { get; set; }
    }

    public class FileVersionSource : VersionSource
    {
        public string FilePath { get; set; }
    }
}
