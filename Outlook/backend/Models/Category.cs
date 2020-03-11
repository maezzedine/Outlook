using backend.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace backend.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
