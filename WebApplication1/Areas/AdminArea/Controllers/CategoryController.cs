using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminArea.ViewModels.Category;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.categories.AsNoTracking().ToListAsync();
            return View(categories);
        }  
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(c=>c.Id==id);
            if (category == null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View(category);
            if(await _context.categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "The Name already exists");
                return View(category);
            }
            var newCategory = new Category()
            {
                Name = category.Name,
                Desc = category.Desc,
                CreatedDate = DateTime.Now,
            };
           await _context.categories.AddAsync(newCategory);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            return View(new CategoryUpdateVM
            {
                Name=category.Name,
                Desc=category.Desc,
            });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async  Task<IActionResult> Update(int? id, CategoryUpdateVM categoryUpdateVM)
        {
            if (id == null) return BadRequest();
            if (!ModelState.IsValid) return View(categoryUpdateVM);
            var category = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            var categoryWithName = _context.categories.Any(c => c.Name.ToLower() == categoryUpdateVM.Name.ToLower() && c.Id!=id);
            if (categoryWithName)
            {
                ModelState.AddModelError("Name", "Such name already exists");
                return View(categoryWithName);
            }
            category.Name = categoryUpdateVM.Name;
            category.Desc = categoryUpdateVM.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
