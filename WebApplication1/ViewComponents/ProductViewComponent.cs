using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;

namespace WebApplication1.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take =3)
        {
            var products = _context.products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Take(take).ToList();
            return View(await Task.FromResult(products));
        }
    }
}
