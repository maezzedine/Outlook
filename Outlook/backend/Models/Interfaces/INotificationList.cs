using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface INotificationList
    {
        public int Id { get; set; }
        public Queue<INotification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public void AddNotification(INotification notification);
        public void MarkAllSeen();
    }
}
