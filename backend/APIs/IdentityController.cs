using AutoMapper;
using backend.Areas.Identity;
using backend.Entities;
using backend.Models.Dtos;
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
        private readonly IMapper mapper;

        public IdentityController(
            UserManager<OutlookUser> userManager, 
            IdentityService identityService,
            IEmailSender emailSender,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.emailSender = emailSender;
            this.mapper = mapper;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.web);
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

        /// <summary>
        /// Change the password of a user given his Bearer token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/identity/changepassword
        ///       "Content-Type": "application/json",
        ///       "Authorization": `Bearer ${token}`,
        ///         "body": {
        ///             "OldPassword": "testOldPassword"
        ///             "NewPassword": "testNewPassword"
        ///         }
        /// 
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="200">Returns the change password results</response>
        /// <response code="400">Returns Bad Request</response>
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

        /// <summary>
        /// Gets a specific user given its Bearer token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/articles/rateuparticle/1
        ///     {
        ///         "Content-Type": "application/json",
        ///         "Authorization": `Bearer ${token}`
        ///     }
        /// 
        /// </remarks>
        /// <returns>OutlookUser object</returns>
        /// <response code="200">Returns the user's information</response>
        /// <response code="404">Returns NotFount result if no user with the given token was found</response>
        [HttpPost("GetUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser()
        {
            var user = await identityService.GetUserWithToken(HttpContext);
            if (user != null)
            {
                return Ok(mapper.Map<UserDto>(user));
            }
            return NotFound();
        }
    }
}