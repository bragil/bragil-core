using Bragil.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TwitterStream.Infra.Messages
{
    /// <summary>
    /// Grava as informações da mensagem em log.
    /// </summary>
    public class LogSender : IMessageSender
    {
        private readonly ILogger logger;

        public LogSender(ILogger<LogSender> logger)
            => this.logger = logger;

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            logger.LogInformation("Enviando mensagem...");
            logger.LogInformation($"From: {from}");
            logger.LogInformation($"To: {to}");
            logger.LogInformation($"Subject: {subject}");
            logger.LogInformation($"Body: {body}");
            logger.LogInformation("Mensagem enviada!");

            await Task.CompletedTask;
        }
    }
}
