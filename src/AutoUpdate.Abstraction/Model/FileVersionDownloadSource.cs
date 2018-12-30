namespace AutoUpdate.Abstraction.Model
{
    public class FileVersionDownloadSource : VersionDownloadSource
    {
        public string FilePath { get; set; }

        public bool IsZipFile { get; set; }

        public override void Accept(IVersionDownloadSourceVisitor visitor)
            => visitor.Handle(this);

        public override TReturn Accept<TReturn>(IVersionDownloadSourceVisitor<TReturn> visitor)
            => visitor.Handle(this);
    }
}
