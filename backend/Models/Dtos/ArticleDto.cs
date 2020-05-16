namespace backend.Models.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }

        public string Language { get; set; }
        
        public CategorySummaryDto Category { get; set; }
        
        public IssueSummaryDto Issue { get; set; }
        
        public MemberSummaryDto Member { get; set; }
        
        public string Title { get; set; }
        
        public string Subtitle { get; set; }
        
        public string PicturePath { get; set; }
        
        public string Text { get; set; }

        public int Rate { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFavorites { get; set; }
    }
}
