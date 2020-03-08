﻿using backend.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Comment : IComment, IRatedBlog
    {
        public int Id { get; set; }
        public IMember Owner { get; set; }
        public string Text { get; set; }


        public DateTime DateTime { get; set; }
        public int Rate { get; set; }
        public int NumberOfVotes { get; set; }
        public int NumberOfFavorites { get; set; }
        [Display(Name = "Replies")]
        public ICollection<IComment> Comments { get; set; }

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
