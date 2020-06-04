using Outlook.Models.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Outlook.Models.Core.Dtos
{
    public class ArticleDto : IArticle<MemberSummaryDto, IssueDto, CategorySummaryDto, CommentDto>
    {
        public int Id { get; set; }

        public string Language { get; set; }

        public CategorySummaryDto Category { get; set; }

        public IssueDto Issue { get; set; }

        public MemberSummaryDto Writer { get; set; }

        public List<CommentDto> Comments { get; set; }

        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string PicturePath { get; set; }

        public string Text { get; set; }

        public int Rate { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFavorites { get; set; }
    }
}
