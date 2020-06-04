using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Outlook.Models.Core.Models;
using Outlook.Models.Core.Dtos;
using Outlook.Services;
using Outlook.Models.Core.Entities;

namespace Outlook.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<OutlookUser> userManager;
        private readonly IdentityService identityService;
        private readonly Logger.Logger logger;
        private readonly IMapper mapper;

        public IdentityController(
            UserManager<OutlookUser> userManager, 
            IdentityService identityService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.mapper = mapper;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.web);
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