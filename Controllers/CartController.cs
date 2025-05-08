using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.Helpers;
using TECHNOVA.ViewModels;
using TECHNOVA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TECHNOVA.Services;

namespace TECHNOVA.Controllers
{
    public class CartController : Controller
    {
        private readonly TechnovaContext db;
        private readonly IEmailService _emailService;

        public CartController(TechnovaContext context, IEmailService emailService)
        {
            db = context;
            _emailService = emailService;
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

        [HttpPost]
        public IActionResult UpdateQuantity([FromBody] UpdateQuantityRequest request)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY);

            if (cart == null)
            {
                return BadRequest(new { success = false, message = "Cart not found" });
            }

            var item = cart.FirstOrDefault(x => x.productId == request.productId);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Item not found in cart" });
            }

            item.quantity = request.quantity;

            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return Ok(new { success = true });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var gioHang = Cart;
            if (gioHang.Count == 0)
            {
                return Redirect("/");
            }

            return View(gioHang);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model, string payment)
        {
            // Kiểm tra dữ liệu form
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Invalid form data: " + string.Join("; ", errors) });
            }

            // Kiểm tra CustomerId
            var customerId = HttpContext.User.Claims.FirstOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
            if (string.IsNullOrEmpty(customerId))
            {
                var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
                return Json(new { success = false, message = "Customer ID not found in claims. Available claims: " + string.Join(", ", claims) });
            }

            // Kiểm tra khách hàng trong database
            var customer = db.Customers.SingleOrDefault(kh => kh.CustomerId == customerId);
            if (customer == null)
            {
                return Json(new { success = false, message = $"Customer with ID {customerId} not found in database" });
            }

            // Kiểm tra dữ liệu khách hàng nếu dùng thông tin khách hàng
            if (model.IsChecked)
            {
                if (string.IsNullOrWhiteSpace(customer.FullName) || string.IsNullOrWhiteSpace(customer.Address) || string.IsNullOrWhiteSpace(customer.Phone))
                {
                    return Json(new { success = false, message = "Customer information is incomplete. Please update your full name, address, and phone in your account." });
                }
            }
            else
            {
                // Kiểm tra các trường bắt buộc nếu không dùng thông tin khách hàng
                if (string.IsNullOrWhiteSpace(model.fullName) || string.IsNullOrWhiteSpace(model.address) || string.IsNullOrWhiteSpace(model.phone))
                {
                    return Json(new { success = false, message = "Full name, address, and phone are required when not using customer information" });
                }
            }

            // Kiểm tra giỏ hàng
            if (!Cart.Any())
            {
                return Json(new { success = false, message = "Cart is empty" });
            }


            // Tạo đơn hàng
            var order = new Order
            {
                CustomerId = customerId,
                FullName = model.IsChecked ? customer.FullName : model.fullName,
                Address = model.IsChecked ? customer.Address : model.address,
                Phone = model.IsChecked ? customer.Phone : model.phone,
                OrderDate = DateTime.Now,
                PaymentMethod = payment ?? "COD",
                ShippingMethod = "GRAB",
                OrderStatus = 1,
                Notes = model.note
            };

            using var transaction = db.Database.BeginTransaction();
            try
            {
                // Debug dữ liệu trước khi lưu
                System.Diagnostics.Debug.WriteLine($"Saving Order: CustomerId={order.CustomerId}, FullName={order.FullName}, Address={order.Address}, Phone={order.Phone}, OrderStatus={order.OrderStatus}");

                // Lưu đơn hàng
                db.Add(order);
                db.SaveChanges();

                // Tạo chi tiết đơn hàng
                var cthds = new List<OrderDetail>();
                foreach (var item in Cart)
                {
                    cthds.Add(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        Quantity = item.quantity,
                        UnitPrice = item.price,
                        ProductId = item.productId,
                        Discount = 0
                    });
                }

                db.AddRange(cthds);
                db.SaveChanges();


                // Gửi email thông báo
                var htmlContent = "<p>Your order has been successfully placed.</p>";
                var plainTextContent = "Your order has been successfully placed.";
                await _emailService.SendEmailAsync(
                    customer.Email,
                    "Technova Shop - Order Confirmation",
                    htmlContent,
                    plainTextContent
                );

                // Xóa giỏ hàng
                HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
                transaction.Commit();

                return View("Success");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                // Ghi log chi tiết lỗi
                var errorMessage = $"Error saving order: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner exception: {ex.InnerException.Message}";
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.InnerException.StackTrace}");
                }
                System.Diagnostics.Debug.WriteLine(errorMessage);
                return Json(new { success = false, message = errorMessage });
            }
        }


        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
