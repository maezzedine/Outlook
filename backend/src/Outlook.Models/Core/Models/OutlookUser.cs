using Microsoft.AspNetCore.Identity;
using Outlook.Models.Core.Relations;
using System.Collections.Generic;

namespace Outlook.Models.Core.Models
{
    public class OutlookUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int NumberOfComments { get; set; }

        public int NumberOfReactions { get; set; }

        public int NumberOfFavoritedArticles { get; set; }

        public List<UserFavoriteArticle> Favorites { get; set; }

        public List<UserRateArticle> Rates { get; set; }
    }
}