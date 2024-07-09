using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.AdminArea.ViewModels.Product;
using WebApplication1.DAL;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prodcuts = await _context.products
                .Include(p=>p.ProductImages)
                .Include(p=>p.Category)
                .AsNoTracking()
                .ToListAsync();
            return View(prodcuts);
        }
        public async Task<IActionResult> Create() 
        {
            ViewBag.Categories = new SelectList (await _context.categories.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM) 
        {
            ViewBag.Categories = new SelectList(await _context.categories.ToListAsync(), "Id", "Name");
            if (!ModelState.IsValid) return View();
            var files = productCreateVM.Photos;
            if (files.Length ==0)
            {
                ModelState.AddModelError("Photos", "Can't be empty");
                return View(productCreateVM);
            }
            Product newProduct = new();
            List<ProductImage> list = new();
            foreach (var file in files)
            {
                if (!file.CheckContentType())
                {
                    ModelState.AddModelError("Photos", "Invalid file type");
                    return View(productCreateVM);
                }
                if (file.CheckSize(1000))
                {
                    ModelState.AddModelError("Photos", "Exceeds the maxsimum size");
                    return View(productCreateVM);
                }
                ProductImage productImage = new ();
                productImage.ImageUrl= await file.SaveFile();
                productImage.ProductId=newProduct.Id;
                if (files[0] == file)
                {
                    productImage.IsMain = true;
                }
                list.Add(productImage);
            }
            newProduct.ProductImages = list;
            newProduct.Name = productCreateVM.Name;
            newProduct.CategoryId = productCreateVM.CategoryId;
            newProduct.Price = productCreateVM.Price;
            newProduct.Count = productCreateVM.Count;
            await _context.products.AddAsync(newProduct);
            await _context.SaveChangesAsync();  
            return RedirectToAction("Index");
        }
    }
}
