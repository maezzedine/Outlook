using backend.Models;
using System.Collections.Generic;

namespace backend.Areas.Identity
{
    public interface IUser
    {
        public string Name { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }

        public Queue<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public void AddNotification(Notification notification);
        public void MarkAllNotificationSeen();
    }
    
}