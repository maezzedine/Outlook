using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class DataFactory
    {
        private readonly OutlookContext context;
        private readonly UserManager<Member> userManager;
        private readonly SignInManager<Member> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public static IConfiguration Configuration;

        public DataFactory(
            OutlookContext context, 
            UserManager<Member> userManager, 
            SignInManager<Member> signInManager, 
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
        public async Task<Member> CreateUser(string username)
        {
            Member admin = new Member
            {
                Name = username,
                EmailConfirmed = true,
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Position = Models.Interfaces.Position.Admin,
            };

            var oldAdmin = from member in userManager.Users
                           where member.UserName == admin.UserName
                           select member;

            if (oldAdmin.FirstOrDefault() == null)
            {
                var adminPassword = Configuration.GetValue<string>("AdminPassword");
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
        }

        public async Task AssignUserRole(Member user, string roleName)
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
