using backend.Models.Interfaces;
using System;

namespace backend.Models
{
    public class Notification : INotification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public string MemberID { get; set; }
    }
}
