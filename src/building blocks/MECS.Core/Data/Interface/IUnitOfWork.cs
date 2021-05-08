using System.Threading.Tasks;

namespace MECS.Core.Data.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
