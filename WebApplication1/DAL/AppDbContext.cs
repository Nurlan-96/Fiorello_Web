using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Slider> sliders { get; set; }
        public DbSet<SliderContent> sliderContents { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
