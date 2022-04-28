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
        public async Task SendAsync(string to, string subject, string text, string from = "online.school.project.2022@gmail.com")
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? "online.school.project.2022@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = text };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("online.school.project.2022", "rihlornqvcygmzdd");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
