namespace backend.Models.Interfaces
{
    public interface IArticle
    {
        public Language Language { get; set; }
        public ICategory Category { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public IMember Writer { get; set; }
    }
    public enum Language
    {
        Arabic,
        English
    }
}
