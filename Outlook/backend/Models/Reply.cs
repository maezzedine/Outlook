using backend.Models.Interfaces;
using System;

namespace backend.Models
{
    public class Reply : IComment
    {
        public int Id { get; set; }
        public Member Owner { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public Comment Comment { get; set; }
    }
}
