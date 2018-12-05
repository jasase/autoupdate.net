using System.Threading.Tasks;

namespace AutoUpdate.Core.Abstraction
{
    public interface IUpdaterManagementService
    {
        void Start();

        Task<IUpdateVersionHandle> SearchVersion();
    }
}
