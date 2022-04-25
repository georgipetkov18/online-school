using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using OnlineSchoolBusinessLogic.Interfaces;

namespace OnlineSchoolBusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Send(string to, string subject, string text, string? from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? configuration.GetValue<string>("EmailSender:EmailFrom")));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = text };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetValue<string>("EmailSender:SmtpHost"), configuration.GetValue<int>("EmailSender:SmtpPort"), SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetValue<string>("EmailSender:SmtpUser"), configuration.GetValue<string>("EmailSender:SmtpPass"));
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
