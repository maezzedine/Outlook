using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int VolumeID { get; set; }
        public string ArabicTheme { get; set; }
        public string EnglishTheme { get; set; }
    }
}
