using backend.Models.Interfaces;
using backend.Validation_Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        [CategoryUniqueness]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [DisplayName("Editors")]
        public IList<Member> JuniorEditors { get; set; }
        public Tag Tag { get; set; }
        [NotMapped]
        public string TagName { get; set; }
        [NotMapped]
        public int ArticlesCount { get; set; }
    }
}
