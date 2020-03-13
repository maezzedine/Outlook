using backend.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Issue : IIssue
    {
        public int Id { get; set; }
        [DisplayName("Issue Number")]
        public int IssueNumber { get; set; }
        [DisplayName("Arabic PDF")]
        public string ar_pdf { get; set; }
        [DisplayName("English PDF")]
        public string en_pdf { get; set; }
        [DisplayName("Arabic Cover")]
        public string ar_cover { get; set; }
        [DisplayName("English Cover")]
        public string en_cover { get; set; }
        public int VolumeID { get; set; }
        [NotMapped]
        public int VolumeNumber { get; set; }
    }
}
