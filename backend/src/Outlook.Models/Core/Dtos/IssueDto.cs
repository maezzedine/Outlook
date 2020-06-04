using Outlook.Models.Core.Interfaces;

namespace Outlook.Models.Core.Dtos
{
    public class IssueDto : IIssueSummary
    {
        public int Id { get; set; }

        public VolumeSummaryDto Volume { get; set; }

        public int Number { get; set; }

        public string ArabicTheme { get; set; }

        public string EnglishTheme { get; set; }
    }
}
