using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public class MailService
    {
        public async Task SendEmailAsync(ContactRequest request)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Portfolio Contact", "sudedogaan1@gmail.com"));
                email.To.Add(new MailboxAddress("Sude", "sudedogaan1@gmail.com"));

                email.Subject = $"Portfolio Contact from {request.Name}";

                email.Body = new TextPart("plain")
                {
                    Text = $"Name: {request.Name}\nEmail: {request.Email}\nMessage: {request.Message}"
                };

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync("sudedogaan1@gmail.com", "APP_PASSWORD");

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SMTP ERROR: " + ex.ToString());
                throw;
            }
        }
    }
}
