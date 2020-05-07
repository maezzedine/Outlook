using backend.Areas.Identity;
using backend.Models.Interfaces;
using System;

namespace backend.Models
{
    public class Comment : IComment, IRatedBlog
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public OutlookUser User { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public int Rate { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFavorites { get; set; }

        public int ArticleID { get; set; }

        public Article Article { get; set; }

        public void RateDown()
        {
            Rate--;
            NumberOfVotes++;
        }

        public void RateUp()
        {
            Rate++;
            NumberOfVotes++;
        }

        public void UnRateDown()
        {
            Rate++;
            NumberOfVotes--;
        }

        public void UnRateUp()
        {
            Rate--;
            NumberOfVotes--;
        }
    }
}
