using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IIssue
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int IssueNumber { get; set; }

        public int VolumeID { get; set; }

        public string ArabicTheme { get; set; }

        public string EnglishTheme { get; set; }

        public List<Article> Articles { get; set; }
    }
}
