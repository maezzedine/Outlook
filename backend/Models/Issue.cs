using backend.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace backend.Models
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
    }
}
