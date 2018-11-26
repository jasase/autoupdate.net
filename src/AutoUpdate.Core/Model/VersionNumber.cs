namespace AutoUpdate.Core.Model
{
    public class VersionNumber
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revision { get; set; }
        public int Build { get; set; }

        public VersionNumber(int major, int minor, int revision, int build)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        public VersionNumber()
            : this(0, 0, 0, 0)
        { }
    }
}
