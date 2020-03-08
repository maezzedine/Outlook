using backend.Models.Interfaces;
using System.Collections.Generic;

namespace backend.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string CategoryName { get; set; }
    }
}
