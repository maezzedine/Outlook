using Outlook.Models.Core.Models;
using static Outlook.Models.Services.OutlookConstants;

namespace Outlook.Models.Core.Relations
{
    public class UserRateArticle
    {
        public int Id { get; set; }

        public OutlookUser User { get; set; }

        public Article Article { get; set; }

        public UserRate Rate { get; set; }
    }
}
