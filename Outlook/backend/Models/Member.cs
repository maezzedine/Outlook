using backend.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        [DisplayName("Category")]
        public string CategoryField { get; set; }
        [NotMapped]
        public Category Category { get; set; }

        public string GetPosition()
        {
            return Position.ToString().Replace('_', ' ');
        }
    }
}
