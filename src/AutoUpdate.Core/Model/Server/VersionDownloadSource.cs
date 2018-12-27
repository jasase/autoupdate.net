namespace AutoUpdate.Core.Model
{
    public abstract class VersionDownloadSource
    {
        public abstract void Accept(IVersionDownloadSourceVisitor visitor);
        public abstract TReturn Accept<TReturn>(IVersionDownloadSourceVisitor<TReturn> visitor);
    }
}

