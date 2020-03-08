namespace backend.Models.Relations
{
    public class MemberFavoritedArticleRelation
    {
        public int ID { get; set; }
        public Member Member { get; set; }
        public int ArticleID { get; set; }
    }
}
