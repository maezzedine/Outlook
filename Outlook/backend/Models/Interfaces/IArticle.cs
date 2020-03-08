using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IArticle
    {
        public Language Language { get; set; }
        public Category Category { get; set; }
        public Issue Issue { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public Member Writer { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    public enum Language
    {
        Arabic,
        English
    }
}
