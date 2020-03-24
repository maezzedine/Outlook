using backend.Models.Interfaces;
using backend.Validation_Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backend.Models.Relations.UserRateArticle;

namespace backend.Models
{
    public class Article : IArticle, IRatedBlog
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public int CategoryID { get; set; }
        [NotMapped]
        public string Category { get; set; }
        public int IssueID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        [Display(Name="Picture")]
        public string PicturePath { get; set; }
        [NotMapped]
        [DisplayName("Delete Picture")]
        public bool DeletePicture { get; set; }
        public string Text { get; set; }
        public int MemberID { get; set; }
        [NotMapped]
        public string Writer { get; set; }
        [NotMapped]
        [DisplayName("New Writer")]
        [MemberUniqueness]
        public string NewWriter { get; set; }
        public DateTime DateTime { get; set; }
        public int Rate { get; set; }
        public int NumberOfVotes { get; set; }
        public int NumberOfFavorites { get; set; }
        [NotMapped]
        public List<Comment> Comments { get; set; }
        [NotMapped]
        public UserRate RatedByUser { get; set; }

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

        public void UpdateArticleInfo(Language lang, int categoryID, string title, string subtitle, int memberID, /*string picture,*/ string text )
        {
            Language = lang;
            CategoryID = categoryID;
            Title = title;
            Subtitle = subtitle;
            MemberID = memberID;
            //Picture = picture;
            Text = text;
        }
    }
}
