using backend.Areas.Identity;
using backend.Models.Interfaces;
using System;

namespace backend.Models
{
    public class Comment : IComment
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public OutlookUser User { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public int ArticleID { get; set; }

        public Article Article { get; set; }
    }
}
