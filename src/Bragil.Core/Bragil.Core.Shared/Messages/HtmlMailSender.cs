using Bragil.Core.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TwitterStream.Infra.Messages
{
    /// <summary>
    /// Evnio de e-mail em formato HTML.
    /// </summary>
    public class HtmlMailSender : IMessageSender
    {
        private readonly SmtpClient smtp;

        public HtmlMailSender(SmtpClient smtp)
            => this.smtp = smtp;

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            await smtp.SendMailAsync(message);
        }
    }
}
