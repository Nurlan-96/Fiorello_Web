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
            var query = _context.blogs.AsQueryable();
            ViewBag.BlogCount= query.Count();
            var datas = query.AsNoTracking().OrderByDescending(b=>b.Id).Take(3).ToList();
            return View(datas);
        }
        public IActionResult Detail (int? id)
        {
            if(id is null) return NotFound();
            var blogs=_context.blogs.AsNoTracking().FirstOrDefault(b=>b.Id == id);
            if(blogs == null) return NotFound();
            return View(blogs);
        }
        public IActionResult LoadMore(int offset = 3)
        {
            var datas=_context.blogs.Skip(offset).Take(3).ToList();
            return PartialView("_BlogPartialView",datas);
        }

        public IActionResult Search(string text)
        {
            var datas = _context.blogs.Where(b=>b.Title.ToLower().Contains(text.ToLower())).OrderByDescending(b=>b.Id).Take(3).ToList();
            return PartialView("_SearchPartialView",datas);
        } 
    }
}
