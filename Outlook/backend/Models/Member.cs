using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace backend.Models
{
    public class Member : IdentityUser, IMember
    {
        public string Name { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public int NumberOfFavoritedArticles { get; set; }
        public Queue<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Position Position { get; set; }
        public double TotalContribution
        {
            get
            {
                return 1.5 * NumberOfReactions + 3 * NumberOfComments + 5 * NumberOfFavoritedArticles + 10 * NumberOfArticles;
            }
        }

        public void AddNotification(Notification notification)
        {
            if (Notifications.Count == 10)
            {
                Notifications.Dequeue();
            }
            Notifications.Enqueue(notification);
            NewNotifications++;
        }
        public void MarkAllNotificationSeen()
        {
            NewNotifications = 0;
        }
        public string GetPosition()
        {
            return Position.ToString().Replace('_', ' ');
        }
    }
}
