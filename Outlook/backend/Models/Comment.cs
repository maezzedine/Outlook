using backend.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class Comment : IComment
    {
        public int Id { get; set; }
        public IMember Owner { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public ICollection<IComment> Replies { get; set; }
    }
}
