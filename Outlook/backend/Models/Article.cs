using backend.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class Article : IArticle, IRatedBlog
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Issue Issue { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public Member Writer { get; set; }
        public DateTime DateTime { get; set; }
        public int Rate { get; set; }
        public int NumberOfVotes { get; set; }
        public int NumberOfFavorites { get; set; }
        public ICollection<Comment> Comments { get; set; }

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
