using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController (AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var products = _context.products
            //.Take(3)
            //.Select(p=>new ProductVM
            //{
            //    Name = p.Name,
            //    Price = p.Price,
            //    CategoryName = p.Category.Name,
            //    MainImageUrl = p.ProductImages.FirstOrDefault(i=>i.IsMain).ImageUrl,
            //}).ToList();
            //List<ProductVM> list = new();
            //foreach (var item in products) 
            //{
            //    ProductVM productVM = new ProductVM();
            //    productVM.Name = item.Name;
            //    productVM.Price = item.Price;
            //    productVM.CategoryName = item.Category.Name;
            //    productVM.MainImageUrl = item.ProductImages.FirstOrDefault(i => i.IsMain)?.ImageUrl;
            //    list.Add(productVM);
            //}
            var products  = _context.products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Take(3).ToList();
            return View(products);
        }
    }
}
