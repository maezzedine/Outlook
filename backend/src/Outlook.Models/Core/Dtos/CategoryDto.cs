using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;

namespace Outlook.Models.Core.Dtos
{
    public class CategorySummaryDto : ICategorySummary
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }
    }

    public class CategoryDto : CategorySummaryDto, ICategory<ArticleDto, MemberDto>
    {
        public List<ArticleDto> Articles { get; set; }
        
        public List<MemberDto> Editors { get; set; }
    }
}
