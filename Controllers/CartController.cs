using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.Helpers;
using TECHNOVA.ViewModels;


namespace TECHNOVA.Controllers
{
    public class CartController : Controller
    {
        private readonly TechnovaContext db;

        public CartController(TechnovaContext context) 
        {
            db = context;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.productId == productId);
            if (item == null)
            {
                var product = db.Products.SingleOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {productId}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    productId = product.ProductId,
                    productName = product.ProductName,
                    price = product.UnitPrice ?? 0,
                    image = product.Image ?? string.Empty,
                    
                    quantity = quantity
                };
                cart.Add(item);
            }
            else
            {
                item.quantity += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int productId)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.productId == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);

            }
            return RedirectToAction("Index");
        }
    }
}
