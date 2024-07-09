using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product:BaseEntity
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public Product()
        {
            ProductImages = new();
        }

        public int Count { get; set; }

    }
}
