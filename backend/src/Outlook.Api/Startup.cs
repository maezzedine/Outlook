using AutoMapper;
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
using Outlook.Api.Hubs;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using Outlook.Models.Services;
using Outlook.Services;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Outlook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p
                    //.WithOrigins(Configuration.GetValue<string>("ClientUrl").Split(';'))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });

            services.AddDbContext<OutlookContext>(options =>
                    options.UseSqlServer(OutlookSecrets.DatabaseConnectionString.Development,
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(OutlookConstants.MigrationsAssembly)));
            // TODO: For production dataase connection: use SqlConnectionStringBuilder to add the database password from the secrets file

            services.AddDefaultIdentity<OutlookUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<OutlookContext>();

            var OutlookModelsAssemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().FirstOrDefault(a => a.Name == "Outlook.Models");
            services.AddAutoMapper(Assembly.Load(OutlookModelsAssemblyName));

            services.AddSignalR();

            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = OutlookConstants.Urls.Development.Server;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "outlookApi";
                    options.RequireHttpsMetadata = false; // todo: uncomment when ssl is available
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ClockSkew = TimeSpan.FromMinutes(0)
                        };
                });

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

            // Add app services
            services.AddTransient<ArticleService>();
            services.AddTransient<IdentityService>();
            services.AddTransient<MemberService>();

            // Add email sending functionality
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(OutlookConstants.OpenSource.Version, new OpenApiInfo
                {
                    Title = "Outlook API",
                    Version = OutlookConstants.OpenSource.Version,
                    Description = "Documentation of Outlook RESTful API and the schema of the Outlook Database.",
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri(OutlookConstants.OpenSource.LicenseUrl)
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
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{OutlookConstants.OpenSource.Version}/swagger.json", "Outlook API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder =>
                    builder
                        .WithOrigins(OutlookConstants.Urls.Development.Client.Split(';'))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ArticleHub>("/article-hub");
            });
        }
    }
}
