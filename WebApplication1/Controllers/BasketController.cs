using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class BasketController:Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }             
        public IActionResult AddBasket(int? id)
        {
        if(id==null) return BadRequest();
        var existProduct=_context.products.FirstOrDefault(p => p.Id==id);
            if (existProduct == null) return BadRequest();
            string basket = Request.Cookies["basket"];
            List<BasketVM> list;
            if (basket is null)
            {
                list = new();
            }
            else
            { 
            list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);    
            }
            var existProductBasket=list.FirstOrDefault(p=>p.Id==existProduct.Id);
            if (existProductBasket == null)
            {
                list.Add(new BasketVM()
                {
                    Id = existProduct.Id,
                    BasketCount = 1
                });
            }
            else
            {
                existProductBasket.BasketCount++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(list));
            return RedirectToAction("Index","Home");
        }
        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> list;
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var basketProduct in list)
                {
                    var existProduct = _context.products
                        .Include(p=>p.ProductImages)
                        .FirstOrDefault(p => p.Id == basketProduct.Id);
                    basketProduct.Name = existProduct.Name;
                    basketProduct.Image = existProduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl;
                }

            }
            return View(list);
        }
    }
}
