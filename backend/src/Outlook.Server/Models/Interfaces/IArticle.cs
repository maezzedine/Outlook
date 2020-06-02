using Microsoft.AspNetCore.Http;

namespace Outlook.Server.Models.Interfaces
{
    public interface IArticle
    {
        public Language Language { get; set; }

        public Category Category { get; set; }

        public Issue Issue { get; set; }

        public Member Member { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string PicturePath { get; set; }

        public string Text { get; set; }
    }
    public enum Language
    {
        Arabic,
        English
    }
}
