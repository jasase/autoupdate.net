namespace AutoUpdate.Core.Model
{
    public class VersionNumber
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }

        public VersionNumber(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public VersionNumber()
            : this(0, 0, 0, 0)
        { }
    }
}
