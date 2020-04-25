namespace backend.Models.Interfaces
{
    public interface ICategory
    {
        public int Id { get; set; }
        
        public Language Language { get; set; }
        
        public string CategoryName { get; set; }
        
        public Tag Tag { get; set; }
    }
    public enum Tag
    {
        News, Lifestyle, Opinion, Art, Culture, Personality, Gender, Society, Cover, Other
    }
}