namespace AutoUpdate.Abstraction
{
    public interface IUserInteraction
    {
        void NewVersionAvailable(IUpdateVersionHandle handle);
    }
}
