using backend.Areas.Identity;

namespace backend.Models.Relations
{
    public class UserFavoritedArticleRelation
    {
        public int ID { get; set; }
        
        public OutlookUser User { get; set; }
        
        public Article Article { get; set; }
    }
}
