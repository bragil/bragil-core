using System.Collections.Generic;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para regras de validação de um objeto.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto</typeparam>
    public interface IValidatorRules<T>
    {
        bool IsValid(T obj);
        List<string> ErrorMessages(T obj);
    }
}
