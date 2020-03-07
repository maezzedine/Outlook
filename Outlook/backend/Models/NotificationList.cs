using backend.Models.Interfaces;
using System.Collections.Generic;

namespace backend.Models
{
    public class NotificationList : INotificationList
    {
        public int Id { get; set; }
        public Queue<INotification> Notifications { get; set; }
        public int NewNotifications { get; set; }

        public void AddNotification(INotification notification)
        {
            if (Notifications.Count == 10)
            {
                Notifications.Dequeue();
            }
            Notifications.Enqueue(notification);
            NewNotifications++;
        }

        public void MarkAllSeen()
        {
            NewNotifications = 0;
        }
    }
}
