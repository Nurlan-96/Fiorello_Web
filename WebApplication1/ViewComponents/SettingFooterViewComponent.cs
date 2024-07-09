using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;

namespace WebApplication1.ViewComponents
{
    public class SettingFooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public SettingFooterViewComponent(AppDbContext context)
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
