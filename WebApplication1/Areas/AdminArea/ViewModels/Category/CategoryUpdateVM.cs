using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.AdminArea.ViewModels.Category
{
    public class CategoryUpdateVM
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Desc { get; set; }
    }
}
