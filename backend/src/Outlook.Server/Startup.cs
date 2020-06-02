using AutoMapper;
using Outlook.Server.Areas.Identity;
using Outlook.Server.Data;
using Outlook.Server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Outlook.Models.Data;
using Outlook.Services;
using Outlook.Models.Core.Models;

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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p
                    //.WithOrigins(Configuration.GetValue<string>("ClientUrl").Split(';'))
                    .AllowAnyHeader() 
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddSignalR();

            services.AddDbContext<OutlookContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OutlookContext")));
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

            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["ApplicationUrls:Server"];
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "outlookApi";
                    options.RequireHttpsMetadata = false; // todo: uncomment when ssl is available
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.FromMinutes(0)
                    };
                });

            // Add app services
            services.AddTransient<ArticleService>();
            services.AddTransient<IdentityService>();
            services.AddTransient<MemberService>();

            // Add email sending functionality
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Open-Source:Version"], new OpenApiInfo { 
                    Title = "Outlook API", 
                    Version = Configuration["Open-Source:Version"],
                    Description = "Documentation of Outlook RESTful API and the schema of the Outlook Database.",
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri(Configuration["Open-Source:License"])
                    }
                });

                // Set the XML comments path for the Swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Configuration["Open-Source:Version"]}/swagger.json", "Outlook API");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(builder =>
                    builder
                        .WithOrigins(Configuration["ApplicationUrls:Clients"].Split(';'))
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
                endpoints.MapHub<ArticleHub>("/article-hub");
            });
        }
    }
}
