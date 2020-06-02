using Outlook.Models.Core.Interfaces;
using System;

namespace Outlook.Models.Core.Models
{
    public class Comment : IComment
    {
        public int Id { get; set; }

        public OutlookUser User { get; set; }

        public Article Article { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Text { get; set; }
    }
}
