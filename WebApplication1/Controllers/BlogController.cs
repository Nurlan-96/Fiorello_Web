using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.blogs.AsNoTracking().OrderByDescending(b=>b.Id).ToList());
        }
        public IActionResult Detail (int? id)
        {
            if(id is null) return NotFound();
            var blogs=_context.blogs.AsNoTracking().FirstOrDefault(b=>b.Id == id);
            if(blogs == null) return NotFound();
            return View(blogs);
        }
    }
}
