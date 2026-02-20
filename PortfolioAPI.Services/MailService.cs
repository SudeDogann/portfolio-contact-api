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
            try
            {
                var email = new MimeMessage();

                // Environment'tan verileri çekiyoruz
                var smtpUser = _configuration["SMTP_USER"];
                var smtpPass = _configuration["SMTP_PASS"];
                var smtpHost = _configuration["SMTP_HOST"];
                var smtpPort = int.Parse(_configuration["SMTP_PORT"] ?? "587");

                email.From.Add(new MailboxAddress("Portfolio Contact", smtpUser));
                email.To.Add(new MailboxAddress("Sude", smtpUser));
                email.Subject = $"Portfolio Contact from {request.Name}";

                email.Body = new TextPart("plain")
                {
                    Text = $"Name: {request.Name}\nEmail: {request.Email}\nMessage: {request.Message}"
                };

                using var smtp = new SmtpClient();
                smtp.Timeout = 60000;
                // Render (Linux) üzerinde sertifika hatalarını önlemek için kritik satır:
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.LocalDomain = "localhost";

                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(smtpUser, smtpPass);
                await smtp.SendAsync(email);
               
            }
            catch (Exception ex)
            {
                // Render loglarında hatayı görmek için
                Console.WriteLine($"Mail Error: {ex.Message}");
                throw;
            }
        }
    }
}
