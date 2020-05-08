using backend.Areas.Identity;
using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<OutlookUser> userManager;
        private readonly IdentityService identityService;
        private readonly Logger.Logger logger;
        private readonly IEmailSender emailSender;

        public IdentityController(
            UserManager<OutlookUser> userManager, 
            IdentityService identityService,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.emailSender = emailSender;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.web);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
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

        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await identityService.GetUserWithToken(HttpContext);
                if (user != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        logger.Log($"User {user.UserName} changed their password.");
                    }

                    return new JsonResult(result);
                }
            }

            return BadRequest();
        }

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

        [HttpPost("GetUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser()
        {
            var user = await identityService.GetUserWithToken(HttpContext);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

    }
}