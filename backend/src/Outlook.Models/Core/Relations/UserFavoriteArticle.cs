using Outlook.Models.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outlook.Models.Core.Relations
{
    public class UserFavoriteArticle
    {
        public int Id { get; set; }

        public OutlookUser User { get; set; }

        public Article Article { get; set; }
    }
}
