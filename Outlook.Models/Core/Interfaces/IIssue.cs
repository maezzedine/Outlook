using System.Collections.Generic;

namespace Outlook.Models.Core.Interfaces
{
    public interface IIssueSummary
    {
        public int Number { get; set; }

        public string ArabicTheme { get; set; }

        public string EnglishTheme { get; set; }
    }

    public interface IIssue<T> : IIssueSummary where T : IArticleSummary
    {
        public List<T> Articles { get; set; }
    }

    public interface IIssue : IIssueSummary
    {
        public List<IArticleSummary> Articles { get; set; }
    }
}
