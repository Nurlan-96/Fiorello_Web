using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminArea.ViewModels.Slider;
using WebApplication1.DAL;
using WebApplication1.Extensions;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM sliderCreateVM)
        {
            var file = sliderCreateVM.Photo;
            if (file == null)
            {
                ModelState.AddModelError("Photo", "Can't be empty");
                return View(sliderCreateVM);
            }
            if (!file.CheckContentType())
            {
                ModelState.AddModelError("Photo", "Invalid file type");
                return View(sliderCreateVM);
            }
            if (file.CheckSize(1000))
            {
                ModelState.AddModelError("Photo", "Exceeds the maxsimum size");
                return View(sliderCreateVM);
            }
            
            Slider slider = new Slider();
            slider.ImageUrl = await file.SaveFile();
            await _context.sliders.AddAsync(slider);
            await _context.SaveChangesAsync();  
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return BadRequest();
            var slider = await _context.sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update (int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _context.sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();
            return View(new SliderUpdateVM {ImageUrl=slider.ImageUrl});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update (int? id, SliderUpdateVM sliderUpdateVM)
        {
            if (id == null) return BadRequest();
            var slider = await _context.sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();
            var file = sliderUpdateVM.Photo;
            if(file == null)
            {
                ModelState.AddModelError("Photo", "Can't be empty");
                sliderUpdateVM.ImageUrl=slider.ImageUrl;
                return View(sliderUpdateVM);
            }
            if (!file.CheckContentType())
            {
                ModelState.AddModelError("Photo", "Invalid file type");
                return View(sliderUpdateVM);
            }
            if (file.CheckSize(1000))
            {
                ModelState.AddModelError("Photo", "Exceeds the maxsimum size");
                return View(sliderUpdateVM);
            }
            string fileName = await file.SaveFile();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            slider.ImageUrl = fileName;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    
    }
}
