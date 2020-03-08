using backend.Models.Interfaces;
using System.Collections.Generic;

namespace backend.Models
{
    public class Member : IMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public INotificationList NotificationList { get; set; }
        public ICollection<IArticle> FavoratedArticles { get; set; }
        public Position Position { get; set; }

        public string GetPosition()
        {
            return Position.ToString().Replace('_', ' ');
        }
    }
}
