using Outlook.Models.Core.Interfaces;
using Outlook.Models.Core.Models;
using System;

namespace Outlook.Models.Core.Dtos
{
    public class CommentDto : IComment
    {
        public int Id { get; set; }
        
        public OutlookUser User { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
