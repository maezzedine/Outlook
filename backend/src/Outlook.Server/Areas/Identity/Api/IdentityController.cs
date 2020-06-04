using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Entities;
using Outlook.Models.Core.Models;
using Outlook.Services;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Outlook.Server.Areas.Identity.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<OutlookUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly Logger.Logger logger;

        public IdentityController(
            UserManager<OutlookUser> userManager,
            IdentityService identityService,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.userArticles);
        }

        /// <summary>
        /// Registers a new account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/identity/register
        ///      {
        ///         "Content-Type": "application/json",
        ///         "body": {
        ///             "Username": "test",
        ///             "Email": "test@auboutlook.com",
        ///             "FirstName": "testFirstName",
        ///             "LastName": "testLastName",
        ///             "Password": "testPassword"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="registerModel">Retrieved from the body of the request</param>
        /// <response code="200">Returns the registration results</response>
        /// <response code="400">Returns Bad Request</response>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new OutlookUser { UserName = registerModel.Username, Email = registerModel.Email, FirstName = registerModel.FirstName, LastName = registerModel.LastName };
                var result = await userManager.CreateAsync(user, registerModel.Password);

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(registerModel.Email, "Confirm your AUB Outlook Account", EmailSender.EmailVerificationHtmlMessage(HtmlEncoder.Default.Encode(callbackUrl)));

                logger.Log($"User {user.UserName} was created.");

                return new JsonResult(result);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Requests a new email verification message
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/members/resendemailverification
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        /// 
        /// </remarks>
        /// <param name="username"></param>
        /// <response code="200">The email verification message has been sent successfully</response>
        /// <response code="404">Returns NotFount result if no member with the given username, whose email isn't verified, was found</response>
        [HttpPost("ResendVerification/{username}")]
        public async Task<IActionResult> ResendVerification(string username)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null && !user.EmailConfirmed)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(user.Email, "Confirm your AUB Outlook Account", EmailSender.EmailVerificationHtmlMessage(HtmlEncoder.Default.Encode(callbackUrl)));

                return Ok();
            }
            return NotFound();
        }
    }
}
