using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SendEmailAPI.Helpers;
using SendEmailAPI.Models.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net;

namespace SendEmailAPI.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly SmtpSettings  _smtpSettings;

        public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task<string> SendEmailAsync(string recipientEmail, string recipientFirstName, string Link)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));
            message.Subject = "Enviar um Email simples";
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = "Este é apenas um teste para enviar mensagens apartir do .NET 6"
            };

            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_smtpSettings.Server,_smtpSettings.Port,true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail,_smtpSettings.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return "Email enviado com sucesso";
            }
            catch(Exception e)
            {

                throw e;
            }
            finally
            {
                client.Dispose();
            }

        }
    }
}
