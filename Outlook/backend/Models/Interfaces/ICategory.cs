using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface ICategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<IMember> CategoryEditors { get; set; }
        public ICollection<IArticle> Articles { get; set; }
    }
}