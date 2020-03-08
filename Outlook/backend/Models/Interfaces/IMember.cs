using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IMember
    {
        public string Name { get; set; }
        public int NumberOfArticles { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
        public double TotalContributions
        {
            get
            {
                return NumberOfReactions + 3 * NumberOfComments + 5 * FavoratedArticles.Count + 10 * NumberOfArticles;
            }
        }
        public INotificationList NotificationList { get; set; }
        public ICollection<IArticle> FavoratedArticles { get; set; }
        public Position Position { get; set; }

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