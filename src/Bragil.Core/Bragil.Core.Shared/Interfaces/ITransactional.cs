using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    public interface ITransactional
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }

    public interface ITransactionalAsync
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
