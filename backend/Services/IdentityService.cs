using backend.Areas.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace backend.Services
{
    public class IdentityService
    {
        public static async Task<OutlookUser> GetUserWithToken(UserManager<OutlookUser> userManager, HttpContext context) {
            var username = context.User.FindFirst("name")?.Value;
            var user = await userManager.FindByNameAsync(username);
            return user;
        }
    }
}
