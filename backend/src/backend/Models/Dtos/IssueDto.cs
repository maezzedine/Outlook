using System.Collections.Generic;

namespace backend.Models.Dtos
{
    public class IssueDto
    {
        public int Id { get; set; }

        public VolumeSummaryDto Volume { get; set; }

        public int IssueNumber { get; set; }

        public string ArabicTheme { get; set; }

        public string EnglishTheme { get; set; }
    }
}
