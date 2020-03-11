using backend.Areas.Identity;
using backend.Data;

namespace backend.Models.Relations
{
    public class UserFavoritedArticleRelation
    {
        public int ID { get; set; }
        public OutlookUser User { get; set; }
        public int ArticleID { get; set; }
    }
}
