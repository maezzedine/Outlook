using System.Collections;
using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Language Language { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public IMember Writer { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public ICollection<IComment> Comments { get; set; }
    }
}
