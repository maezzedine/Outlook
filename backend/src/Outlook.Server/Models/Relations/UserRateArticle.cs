using Outlook.Server.Areas.Identity;

namespace Outlook.Server.Models.Relations
{
    public class UserRateArticle
    {
        public int ID { get; set; }

        public OutlookUser User { get; set; }

        public Article Article { get; set; }

        public UserRate Rate { get; set; }

        public enum UserRate
        {
            None, Up, Down
        }
    }
}
