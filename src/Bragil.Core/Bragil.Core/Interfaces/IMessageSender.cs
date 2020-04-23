using System.Threading.Tasks;

namespace Bragil.Core.Interfaces
{
    /// <summary>
    /// Interface para o envio de mensagens.
    /// </summary>
    public interface IMessageSender
    {
        Task SendAsync(string from, string to, string subject, string body);
    }
}
