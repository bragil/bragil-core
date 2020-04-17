using System;

namespace Bragil.Core.Exceptions
{
    /// <summary>
    /// Exceção usada para erros de configuração.
    /// </summary>
    public class InvalidConfigException: Exception
    {
        public InvalidConfigException(): base()
        { }

        public InvalidConfigException(string message) : base(message)
        { }

        public InvalidConfigException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
