using System.Collections.Generic;

namespace backend.Models.Dtos
{
    public class CategorySummaryDto
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public string Language { get; set; }

        public int ArticlesCount { get; set; }

        public string CategoryName { get; set; }
    }

    public class CategoryDto : CategorySummaryDto
    {
        public List<ArticleDto> Articles { get; set; }
        
        public List<MemberDto> JuniorEditors { get; set; }
    }
}
