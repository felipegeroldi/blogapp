using BlogApp.WebApp.Events;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BlogApp.WebApp.Handlers
{
    public class EmailHandler : IEventHandler
    {
        private EmailSettings _emailSettings;
        private SmtpClient _smtpClient = null!;

        public EmailHandler(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
            ConfigureSmtpClient();
        }

        private void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient
            {
                Host = _emailSettings.Host,
                Port = _emailSettings.Port
            };

            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential
            {
                UserName = _emailSettings.Email,
                Password = _emailSettings.Password
            };
        }

        public void SendRegistryEmail(object? source, RegistryNotifyEventArgs args)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email),
                Subject = "User registed",
                Body = GenerateRegistryEmailBody(args.Name, args.Contact),
                IsBodyHtml = true
            };

            mailMessage.To.Add(args.Contact);
            _smtpClient.Send(mailMessage);
        }

        private string GenerateRegistryEmailBody(string name, string email)
        {
            var message = new StringBuilder();
            message.AppendLine($"<p>Hello {name}</p>");
            message.AppendLine($"<p>Your accont with email {email} has been created successfully.</p>");

            return message.ToString();
        }
    }

    public interface IEventHandler
    {

        public void SendRegistryEmail(object? source, RegistryNotifyEventArgs args);
    }
}
