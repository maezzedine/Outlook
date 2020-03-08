using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IComment
    {
        public int Id { get; set; }
        public Member Owner { get; set; }
        public string Text { get; set; }
    }
}