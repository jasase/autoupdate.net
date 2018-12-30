namespace AutoUpdate.Abstraction.Model
{
    public interface IVersionDownloadSourceVisitor
    {
        void Handle(HttpVersionDownloadSource downloadSource);
        void Handle(FileVersionDownloadSource downloadSource);
        void Handle(EmptyVersionDownloadSource downloadSource);
    }

    public interface IVersionDownloadSourceVisitor<TReturn>
    {
        TReturn Handle(HttpVersionDownloadSource downloadSource);
        TReturn Handle(FileVersionDownloadSource downloadSource);
        TReturn Handle(EmptyVersionDownloadSource downloadSource);
    }
}
