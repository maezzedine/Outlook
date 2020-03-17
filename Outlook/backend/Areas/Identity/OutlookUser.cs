using backend.Models;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace backend.Areas.Identity
{
    public class OutlookUser : IdentityUser, IUser
    {
        public string Name { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public int NumberOfFavoritedArticles { get; set; }
        public double TotalContribution
        {
            get
            {
                return 1.5 * NumberOfReactions + 3 * NumberOfComments + 5 * NumberOfFavoritedArticles;
            }
        }
    }
}
