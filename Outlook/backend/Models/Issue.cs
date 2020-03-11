using backend.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace backend.Models
{
    public class Issue : IIssue
    {
        public int Id { get; set; }
        [DisplayName("Issue Number")]
        public int IssueNumber { get; set; }
        public string ar_pdf { get; set; }
        public string en_pdf { get; set; }
        public string ar_cover { get; set; }
        public string en_cover { get; set; }
        public Volume Volume { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
