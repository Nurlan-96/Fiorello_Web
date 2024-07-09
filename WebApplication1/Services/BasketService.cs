using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.DAL;
using WebApplication1.Services.Interfaces;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;


        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public List<BasketVM> GetBasketFromCookies()
        {
            List<BasketVM> list = new();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket != null)
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                return list;
            }
            else return list;
        }
        public int BasketCount()=> GetBasketFromCookies().Count();

        public List<BasketVM> GetBasketList()
        {
            var list = GetBasketFromCookies();
            foreach (var basketProduct in list)
            {
                var existProduct = _context.products
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == basketProduct.Id);
                basketProduct.Name = existProduct.Name;
                basketProduct.Image = existProduct.ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl;
            }
            return list;
        }
    }
}
