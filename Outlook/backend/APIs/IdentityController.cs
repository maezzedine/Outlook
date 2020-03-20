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

        

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login() 
        //{
        //    return;
        //}

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel) 
        {
            var user = new OutlookUser { UserName = registerModel.Username, FirstName = registerModel.FirstName, LastName = registerModel.LastName };
            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                FileLogger.FileLogger.Log(config.GetValue<string>("WebsiteLogFilePath"), $"{DateTime.Now} | User created a new account with password.");
                var token = await signInManager.UserManager.GetAuthenticationTokenAsync(user, "Admin", "login");
                //var token = await signInManager.UserManager.GenerateUserTokenAsync(user, "Admin", "login");

                //await signInManager.SignInAsync(user, isPersistent: false);
                return Ok(token);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
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