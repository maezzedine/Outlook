using Outlook.Models.Core.Models;
using System;

namespace Outlook.Models.Core.Interfaces
{
    public interface IComment
    {
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public OutlookUser User { get; set; }
    }
}
