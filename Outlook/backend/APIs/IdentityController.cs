using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Areas.Identity;
using backend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using static backend.Areas.Identity.Pages.Account.AddUserModel;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<OutlookUser> userManager;
        private readonly SignInManager<OutlookUser> signInManager;
        private readonly IConfiguration config;

        public IdentityController(
            UserManager<OutlookUser> userManager,
            SignInManager<OutlookUser> signInManager,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel inputModel)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | Failure attempt to login the account of username {inputModel.Username}.");
                    //return signInManager.ClaimsFactory.CreateAsync().;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel) 
        {
            if (ModelState.IsValid)
            {
                var user = new OutlookUser { UserName = registerModel.Username, FirstName = registerModel.FirstName, LastName = registerModel.LastName };
                var result = await userManager.CreateAsync(user, registerModel.Password);

                FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | User created a new account with password.");

                return new JsonResult(result);
            }

            return BadRequest(ModelState);
        }

        //[HttpPost("Logout")]
        //public async Task<IActionResult> Logout() 
        //{
        //    return;
        //}
    }
}