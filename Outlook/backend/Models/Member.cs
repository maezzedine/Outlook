using backend.Models.Interfaces;

namespace backend.Models
{
    public class Member : IMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }
        public string Position { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public INotificationList NotificationsList { get; set; }
    }
}
