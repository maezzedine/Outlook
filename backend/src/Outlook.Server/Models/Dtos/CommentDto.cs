using Outlook.Server.Areas.Identity;
using System;

namespace Outlook.Server.Models.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        
        public OutlookUser User { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
