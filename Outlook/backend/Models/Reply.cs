using backend.Models.Interfaces;
using System;

namespace backend.Models
{
    public class Reply : IComment
    {
        public int Id { get; set; }
        public string MemberID { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int CommentID { get; set; }
    }
}
