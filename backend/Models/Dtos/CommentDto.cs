using backend.Areas.Identity;
using System;

namespace backend.Models.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        
        public OutlookUser User { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
