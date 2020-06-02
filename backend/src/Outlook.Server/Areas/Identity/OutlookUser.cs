using Outlook.Server.Models.Relations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Outlook.Server.Areas.Identity
{
    public class OutlookUser : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int NumberOfComments { get; set; }
        
        public int NumberOfReactions { get; set; }
        
        public int NumberOfFavoritedArticles { get; set; }

        public List<UserFavoritedArticleRelation> Favorites{ get; set; }

        public List<UserRateArticle> Rates{ get; set; }

        public double TotalContribution
        {
            get
            {
                return 1.5 * NumberOfReactions + 3 * NumberOfComments + 5 * NumberOfFavoritedArticles;
            }
        }
    }
}
