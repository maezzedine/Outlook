using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IMember
    {
        public string Name { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public Position Position { get; set; }

        public Queue<Notification> Notifications { get; set; }
        public int NewNotifications { get; set; }
        public void AddNotification(Notification notification);
        public void MarkAllNotificationSeen();
        public string GetPosition();
    }
    public enum Position
    {
        Editor_In_Chief,
        Senior_Editor,
        Associate_Editor,
        Junior_Editor,
        Proofreader,
        Web_Editor,
        Copy_Editor,
        Staff_Writer,
        رئيس_تحرير,
        المحرر,
        نائب_المحرر,
        رئيس_قسم,
        مدقق_النسخة,
        مدقق_لغوي,
        مدقق_الموقع,
        كاتب_صحفي
    }
}