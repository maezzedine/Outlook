using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography.X509Certificates;
using Outlook.Models.Data;
using Outlook.Services;
using Outlook.Models.Core.Models;
using Outlook.Models.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Outlook.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // TODO: add app secrets to environment variables when production
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<OutlookContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OutlookContext"),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(OutlookConstants.MigrationsAssembly)));
            // TODO: For production dataase connection: use SqlConnectionStringBuilder to add the database password from the secrets file

            services.AddDefaultIdentity<OutlookUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<OutlookContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 15;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            });

            var config = new Config(Configuration);
            // Add IdentityServer4
            var builder = services.AddIdentityServer()
                .AddSigningCredential(new X509Certificate2(".\\outlook.pfx", Configuration["IdentityServer4:CertificatePassword"]))
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddInMemoryApiResources(config.GetApis())
                .AddInMemoryClients(config.GetClients())
                .AddAspNetIdentity<OutlookUser>()
                .AddJwtBearerClientAuthentication()
                .AddProfileService<IdentityProfileService>();

            // Add app services
            services.AddTransient<ArticleService>();
            services.AddTransient<IdentityService>();
            services.AddTransient<MemberService>();

            // Add email sending functionality
            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(builder =>
                    builder
                        .WithOrigins(OutlookConstants.Urls.Development.Client.Split(';'))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
            );

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
