using System;

namespace backend.Models.Interfaces
{
    public interface INotification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public string UserID { get; set; }
    }
}