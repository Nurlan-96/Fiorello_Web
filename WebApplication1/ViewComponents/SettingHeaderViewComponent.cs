using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents
{
    public class SettingHeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public SettingHeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = _context.settings.ToDictionary(key=>key.Key, val=>val.Value);
            return View(await Task.FromResult(settings));
        }
    }
}
