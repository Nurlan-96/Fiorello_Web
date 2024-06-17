﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var data = _context.ChangeTracker.Entries();
            var homeVM = new HomeVM()
            {
                Sliders = _context.sliders.AsNoTracking().ToList(),
                SliderContent = _context.sliderContents.AsNoTracking().SingleOrDefault(),
                Categories = _context.categories.ToList(),
                Products = _context.products.Include(p=>p.ProductImages).ToList(),
                Blogs = _context.blogs.OrderByDescending(b=>b.Id).Take(3).ToList()
            };
            return View(homeVM);
        }
    }
}