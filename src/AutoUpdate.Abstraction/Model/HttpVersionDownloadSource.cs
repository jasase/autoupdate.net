namespace AutoUpdate.Abstraction.Model
{
    public class HttpVersionDownloadSource : VersionDownloadSource
    {
        public string Url { get; set; }
        public bool IsZipFile { get; internal set; }
        public string FileName { get; internal set; }

        public override void Accept(IVersionDownloadSourceVisitor visitor)
            => visitor.Handle(this);

        public override TReturn Accept<TReturn>(IVersionDownloadSourceVisitor<TReturn> visitor)
            => visitor.Handle(this);
    }
}
