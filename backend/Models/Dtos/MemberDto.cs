using Nancy.Json;
using System.Collections.Generic;

namespace backend.Models.Dtos
{
    public class MemberSummaryDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Language { get; set; }
        
        public string Position { get; set; }

        public int numberOfArticles { get; set; }

        public CategorySummaryDto Category { get; set; }

        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }

    public class MemberDto : MemberSummaryDto
    {
        public List<ArticleDto> Articles { get; set; }
    }
}
