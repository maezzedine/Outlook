using backend.Models;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace backend.Areas.Identity
{
    public class OutlookUser : IdentityUser, IUser
    {
        public string Name { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public int NumberOfFavoritedArticles { get; set; }
        public Queue<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public double TotalContribution
        {
            get
            {
                return 1.5 * NumberOfReactions + 3 * NumberOfComments + 5 * NumberOfFavoritedArticles;
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
        
    }
}
