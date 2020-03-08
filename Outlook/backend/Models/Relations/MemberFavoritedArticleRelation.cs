namespace backend.Models.Relations
{
    public class MemberFavoritedArticleRelation
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int ArticleID { get; set; }
    }
}
