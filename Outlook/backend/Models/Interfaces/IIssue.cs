using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Interfaces
{
    interface IIssue
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int IssueNumber { get; set; }
        public string ar_pdf { get; set; }
        public string en_pdf { get; set; }
        public string ar_cover { get; set; }
        public string en_cover { get; set; }
        public Volume Volume { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
