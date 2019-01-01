namespace AutoUpdate.Abstraction.Model
{
    public class HttpVersionDownloadSource : VersionDownloadSource
    {
        public string Url { get; set; }
        public bool IsZipFile { get; set; }
        public string FileName { get; set; }

        public override void Accept(IVersionDownloadSourceVisitor visitor)
            => visitor.Handle(this);

        public override TReturn Accept<TReturn>(IVersionDownloadSourceVisitor<TReturn> visitor)
            => visitor.Handle(this);
    }
}
