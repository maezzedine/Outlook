using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IComment
    {
        public int Id { get; set; }
        public string MemberID { get; set; }
        public string Text { get; set; }
    }
}