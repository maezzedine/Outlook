using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Outlook.Models.Core.Models
{
    public class Issue : IIssue<Article>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public Volume Volume { get; set; }

        [DisplayName("Arabic Theme")]
        public string ArabicTheme { get; set; }
        
        [DisplayName("English Theme")]
        public string EnglishTheme { get; set; }

        public List<Article> Articles { get; set; }

        public Issue SetIssueNumber(int number)
        {
            Number = number;
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
