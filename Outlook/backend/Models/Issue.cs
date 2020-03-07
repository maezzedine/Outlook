using backend.Models.Interfaces;
using System.Collections.Generic;

namespace backend.Models
{
    public class Issue : IIssue
    {
        public int Id { get; set; }
        public int IssueNumber { get; set; }
        public string ar_pdf { get; set; }
        public string en_pdf { get; set; }
        public string ar_cover { get; set; }
        public string en_cover { get; set; }
        public IVolume Volume { get; set; }
        public ICollection<ICategory> Category { get; set; }
    }
}
