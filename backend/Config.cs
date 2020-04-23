using backend.Areas.Identity;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend
{
    public class Config
    {
        public Config(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("outlookApi", "outlookWebApi")
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "Outlook",
                    AllowedCorsOrigins = Configuration.GetValue<string>("ClientUrl").Split(';'),
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 86400,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "outlookApi"
                    },
                }
            };
        }
    }
}
