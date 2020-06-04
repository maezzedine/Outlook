using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;
using System.Text.Json;

namespace Outlook.Models.Core.Dtos
{
    public class MemberSummaryDto : IMemberSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public string Position { get; set; }

        public int numberOfArticles { get; set; }

        public CategorySummaryDto Category { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class MemberDto : MemberSummaryDto, IMember<ArticleDto>
    {
        public List<ArticleDto> Articles { get; set; }
    }
}
