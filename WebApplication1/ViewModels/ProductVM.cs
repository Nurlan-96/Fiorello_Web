using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ProductVM
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public string CategoryName { get; set; }
        public string MainImageUrl { get; set; }
    }
}
