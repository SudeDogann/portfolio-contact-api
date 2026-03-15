using Resend;
using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public class MailService
    {
        private readonly ResendClient _resend;

        public MailService(ResendClient resend)
        {
            _resend = resend;
        }

        public async Task SendEmailAsync(ContactRequest request)
        {
            try
            {
                var email = new EmailMessage
                {
                    From = "onboarding@resend.dev",
                    To = "yourmail@gmail.com",
                    Subject = $"Portfolio message from {request.Name}",
                    HtmlBody = $@"
                        <h2>New Portfolio Message</h2>
                        <p><strong>Name:</strong> {request.Name}</p>
                        <p><strong>Email:</strong> {request.Email}</p>
                        <p><strong>Message:</strong></p>
                        <p>{request.Message}</p>
                    "
                };

                await _resend.EmailSendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mail Error: {ex}");
                throw;
            }
        }
    }
}