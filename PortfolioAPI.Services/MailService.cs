using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PortfolioAPI.Models;
using Microsoft.Extensions.Configuration; // Bunu eklemeyi unutma

namespace PortfolioAPI.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        // Constructor ile Configuration'ı içeri alıyoruz
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(ContactRequest request)
        {
            var smtpUser = _configuration["SMTP_USER"];
            var smtpPass = _configuration["SMTP_PASS"];
            var smtpHost = _configuration["SMTP_HOST"];
            var smtpPort = int.Parse(_configuration["SMTP_PORT"] ?? "587");

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Portfolio Contact", smtpUser));
            email.To.Add(new MailboxAddress("Sude", smtpUser));
            email.Subject = $"Portfolio Contact from {request.Name}";

            email.Body = new TextPart("plain")
            {
                Text = $"Name: {request.Name}\nEmail: {request.Email}\nMessage: {request.Message}"
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUser, smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
