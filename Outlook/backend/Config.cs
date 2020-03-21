using backend.Areas.Identity;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend
{
    public class Config
    {
        //public static List<OutlookUser> GetUsers()
        //{
        //    return new List<OutlookUser>
        //    {
        //        new OutlookUser
        //        {
        //            SubjectId = "1",
        //            Username = "alice@alice.com",
        //            Password = "password"
        //        }
        //    };
        //}

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("outlookApi", "outlookWebApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "Outlook",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 300,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "outlookApi"
                    }
                }
            };
        }
    }
}
