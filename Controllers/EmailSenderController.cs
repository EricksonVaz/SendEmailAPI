using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmailAPI.Helpers;

namespace SendEmailAPI.Controllers
{
    [Route("v1/email")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailSenderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmailAsync(string toEmail)
        {

            try
            {
                var messageStatus = await _emailSender.SendEmailAsync(toEmail, "Erickson", "outlook.com");
                return Ok(messageStatus);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}
