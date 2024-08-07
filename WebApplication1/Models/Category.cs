﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Desc { get; set; }
        public List<Product> Products { get; set; }
    }
}
