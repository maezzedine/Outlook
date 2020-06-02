using Outlook.Server.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Outlook.Server.Models
{
    public class Issue : IIssue
    {
        public int Id { get; set; }

        public int VolumeID { get; set; }

        public Volume Volume { get; set; }

        [DisplayName("Issue Number")]
        public int IssueNumber { get; set; }

        [DisplayName("Arabic Theme")]
        public string ArabicTheme { get; set; }

        [DisplayName("English Theme")]
        public string EnglishTheme { get; set; }

        public List<Article> Articles { get; set; }

        public Issue SetIssueNumber(int number)
        {
            IssueNumber = number;
            return this;
        }

        public Issue SetArabicTheme(string theme)
        {
            ArabicTheme = theme;
            return this;
        }

        public Issue SetEnglishTheme(string theme)
        {
            EnglishTheme = theme;
            return this;
        }
    }
}
