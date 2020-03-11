using backend.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Member : IMember
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int NumberOfArticles { get; set; }
        
        public string GetPosition()
        {
            return Position.ToString().Replace('_', ' ');
        }
    }
}
