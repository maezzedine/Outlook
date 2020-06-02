using Outlook.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Server.Data
{
    public class DataFactory
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;
        private readonly SignInManager<OutlookUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public static IConfiguration Configuration;

        public DataFactory(
            OutlookContext context,
            UserManager<OutlookUser> userManager,
            SignInManager<OutlookUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            Configuration = configuration;
        }

        public async Task SeedData()
        {
            var admin = await CreateUser("Admin");

            await CreateRoles();

            await AssignUserRole(admin, "Admin");
        }
        public async Task<OutlookUser> CreateUser(string username)
        {
            OutlookUser admin = new OutlookUser
            {
                FirstName = username,
                LastName = username,
                EmailConfirmed = true,
                UserName = username,
                NormalizedUserName = username.ToUpper(),
            };

            var oldAdmin = from member in userManager.Users
                           where member.UserName == admin.UserName
                           select member;

            if (oldAdmin.FirstOrDefault() == null)
            {
                var adminPassword = Configuration["Users:Admin"];
                var addAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (addAdmin.Succeeded)
                {
                    await context.SaveChangesAsync();
                    await signInManager.SignInAsync(admin, isPersistent: false);
                }
                return admin;
            }

            return oldAdmin.FirstOrDefault();
        }
        public async Task CreateRoles()
        {
            // Create the Role 'Admin'
            IdentityResult RoleAdd;

            var AdminRole = from role in context.Roles
                            where role.Name == "Admin"
                            select role;

            if (AdminRole.FirstOrDefault() == null)
            {
                RoleAdd = await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await context.SaveChangesAsync();
            }

            var WebEditorRole = from role in context.Roles
                                where role.Name == "Web-Editor"
                                select role;

            if (WebEditorRole.FirstOrDefault() == null)
            {
                RoleAdd = await roleManager.CreateAsync(new IdentityRole { Name = "Web-Editor" });
                await context.SaveChangesAsync();
            }

            var EditorInChiefRole = from role in context.Roles
                                    where role.Name == "Editor-In-Chief"
                                    select role;

            if (EditorInChiefRole.FirstOrDefault() == null)
            {
                RoleAdd = await roleManager.CreateAsync(new IdentityRole { Name = "Editor-In-Chief" });
                await context.SaveChangesAsync();
            }
        }

        public async Task AssignUserRole(OutlookUser user, string roleName)
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
                    await userManager.AddToRoleAsync(user, Role.FirstOrDefault().Name);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
