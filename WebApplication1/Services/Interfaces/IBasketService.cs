using WebApplication1.ViewModels;

namespace WebApplication1.Services.Interfaces
{
    public interface IBasketService
    {
        int BasketCount();
        List<BasketVM> GetBasketList();
        List<BasketVM> GetBasketFromCookies();
    }
}
