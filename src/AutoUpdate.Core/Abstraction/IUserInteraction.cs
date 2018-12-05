namespace AutoUpdate.Core.Abstraction
{
    public interface IUserInteraction
    {
        void NewVersionAvailable(IUpdateVersionHandle handle);
    }
}
