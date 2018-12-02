namespace AutoUpdate.Core.Abstraction
{
    public interface IUpdaterManagementService
    {
        void Start();

        IUpdateVersionHandle SearchVersion();
    }
}
