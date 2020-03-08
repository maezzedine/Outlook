using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface ICategory
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string CategoryName { get; set; }
    }
}