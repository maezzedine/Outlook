namespace Outlook.Server.Models.Interfaces
{
    public interface IComment
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public string Text { get; set; }

        public Article Article { get; set; }
    }
}