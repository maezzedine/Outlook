using ServiceStack.DataAnnotations;

namespace backend.Models.Interfaces
{
    interface IIssue
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int IssueNumber { get; set; }

        public int VolumeID { get; set; }

        public string ArabicTheme { get; set; }

        public string EnglishTheme { get; set; }
    }
}
