namespace AutoUpdate.Core.Model
{
    public class Version
    {
        public string ChangeLog { get; set; }
        public bool Mandatory { get; set; }
        public VersionNumber VersionNumber { get; set; }
        public VersionDownloadSource Source { get; set; }

        public Version()
        {
            ChangeLog = string.Empty;
            Mandatory = true;
            VersionNumber = new VersionNumber();
        }

        public override string ToString()
            => $"{VersionNumber} - {ChangeLog}";
    }
}
