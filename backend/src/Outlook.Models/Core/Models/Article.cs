using Outlook.Models.Core.Interfaces;
using Outlook.Models.Core.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Outlook.Models.Core.Models
{
    public class Article : IArticle<Member, Issue, Category, Comment>, IRatedBlog
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public Category Category { get; set; }

        public Issue Issue { get; set; }

        public Member Writer { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        [DisplayName("Picture")]
        public string PicturePath { get; set; }

        public string Text { get; set; }

        public int Rate { get; set; }

        [DisplayName("Number of Votes")]
        public int NumberOfVotes { get; set; }

        [DisplayName("Number of Favorites")]
        public int NumberOfFavorites { get; set; }

        public DateTime DateTime { get; set; }

        public List<Comment> Comments { get; set; }

        public List<UserFavoriteArticle> Favorites { get; set; }

        public List<UserRateArticle> Rates { get; set; }

        public void UpVote() => Rate++;

        public void UndoUpVote() => Rate--;

        public void DownVote() => Rate--;

        public void UndoDownVote() => Rate++;

        public Article SetLanguage(string language)
        {
            Language = language;
            return this;
        }

        public Article SetCategory(Category category)
        {
            Category = category;
            return this;
        }

        public Article SetTitle(string title)
        {
            Title = title;
            return this;
        }

        public Article SetText(string text)
        {
            Text = text;
            return this;
        }

        public Article SetSubtitle(string subtitle)
        {
            Subtitle = subtitle;
            return this;
        }
    }
}
