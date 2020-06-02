using Outlook.Server.Areas.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Outlook.Server.Services
{
    public class IdentityService
    {
        private readonly UserManager<OutlookUser> userManager;

        public IdentityService(UserManager<OutlookUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// GetUserWithToken is a method that retrievs a user from a given bearer token
        /// </summary>
        /// <returns>OutlookUser object</returns>
        public async Task<OutlookUser> GetUserWithToken(HttpContext context)
        {
            var username = context.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);
            return user;
        }
    }
}
