using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para uma unidade de trabalho.
    /// </summary>
    public interface IUnitOfWork
    {
        void SaveChanges();
        void RejectChanges();
    }

    /// <summary>
    /// Interface para uma unidade de trabalho.
    /// </summary>
    public interface IUnitOfWorkAsync
    {
        Task SaveChanges();
        Task RejectChanges();
    }
}
