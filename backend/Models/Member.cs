﻿using backend.Models.Interfaces;
using backend.Models.Relations;
using backend.Validation_Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Member : IMember
    {
        public int ID { get; set; }

        [MemberUniqueness]
        [RegularExpression(@"^[a-zA-Z.\-\s]*$", ErrorMessage = "Characters, numbers and special symbols are not allowed.")]
        public string Name { get; set; }

        public Position Position { get; set; }

        [NotMapped]
        public int NumberOfArticles { get; set; }

        public List<Article> Articles { get; set; }

        [NotMapped]
        [DisplayName("Category")]
        public string CategoryField { get; set; }

        public Category Category { get; set; }

        [NotMapped]
        public Language Language { get; set; }

        [NotMapped]
        public string PositionName => Position.ToString().Replace('_', ' ');
    }
}
