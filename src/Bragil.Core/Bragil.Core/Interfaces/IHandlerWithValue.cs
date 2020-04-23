using FunSharp;
using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para handlers que retornam valor.
    /// </summary>
    /// <typeparam name="TArg">Tipo do argumento</typeparam>
    /// <typeparam name="TValue">Tipo do valor de retorno</typeparam>
    public interface IHandlerWithValue<TArg, TValue>
    {
        Task<Res<TValue>> Execute(TArg argument);
    }
}
