using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Helpers;
using TECHNOVA.ViewModels;

namespace TECHNOVA.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(p => p.quantity),
                Total = cart.Sum(p => p.total)
            });
        }
    }
}
