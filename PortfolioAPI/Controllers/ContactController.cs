using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Services;
using PortfolioAPI.Models;

namespace PortfolioContactApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly MailService _mailService;

        public ContactController(MailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactRequest request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok("Mail sent successfully");
        }
    }
}