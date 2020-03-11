using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IArticle
    {
        public Language Language { get; set; }
        public int CategoryID { get; set; }
        public int IssueID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public string MemberID { get; set; }
    }
    public enum Language
    {
        Arabic,
        English
    }
}
