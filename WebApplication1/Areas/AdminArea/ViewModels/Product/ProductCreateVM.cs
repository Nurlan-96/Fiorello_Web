﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminArea.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public IFormFile[] Photos { get; set; }
    }
}
