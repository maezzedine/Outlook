namespace backend.Models.Interfaces
{
    public interface IMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }
        public string Position { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public double TotalContributions
        {
            get
            {
                return NumberOfReactions + 3 * NumberOfComments + 10 * NumberOfArticles;
            }
        }
        public INotificationList NotificationList { get; set; }
    }
    public enum Language
    {
        Arabic,
        English
    }
}