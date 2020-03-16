using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace backend.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private readonly SignInManager<OutlookUser> _signInManager;
        private readonly UserManager<OutlookUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly OutlookContext context;

        public AddUserModel(
            UserManager<OutlookUser> userManager,
            SignInManager<OutlookUser> signInManager,
            ILogger<RegisterModel> logger,
            OutlookContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new OutlookUser { UserName = Input.Username, Name = Input.Name };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    if (Input.Username.IndexOf("WebEditor") != -1)
                    {
                        await AssignUserRole(user, "Web-Editor");
                    }
                    else if (Input.Username.IndexOf("EditorInChief") != -1)
                    {
                        await AssignUserRole(user, "Editor-In-Chiefr");
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private async Task AssignUserRole(OutlookUser user, string roleName)
        {
            var Role = from _role in context.Roles
                       where _role.Name == roleName
                       select _role;

            if (Role.FirstOrDefault() != null)
            {
                var userRoleAssigned = from userRole in context.UserRoles
                                       where (userRole.UserId == user.Id) && (userRole.RoleId == Role.FirstOrDefault().Id)
                                       select userRole;

                if (userRoleAssigned.FirstOrDefault() == null)
                {
                    await _userManager.AddToRoleAsync(user, Role.FirstOrDefault().Name);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
