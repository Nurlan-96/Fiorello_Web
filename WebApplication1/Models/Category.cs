using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category:BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
