using FunSharp;
using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para handlers que não retornam valor.
    /// </summary>
    /// <typeparam name="TArg">Tipo do argumento</typeparam>
    public interface IHandler<in TArg>
    {
        Task<Res> Execute(TArg argument);
    }
}
