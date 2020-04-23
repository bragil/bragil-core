using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para jobs que executam em segundo plano.
    /// </summary>
    public interface IBackgroundJob
    {
        int IntervalInMinutes { get; }

        Task Execute();
    }
}
