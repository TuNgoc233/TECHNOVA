using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace TECHNOVA.Controllers
{
    public class ItemController : Controller
    {
        private readonly TechnovaContext db;

        public ItemController(TechnovaContext context)
        {
            db = context;
        }

        public IActionResult Index(int? category, int page = 1)
        {
            int pageSize = 9;

            var query = db.Products.AsQueryable();

            if (category.HasValue)
            {
                query = query.Where(p => p.CategoryId == category.Value);
            }

            int totalItems = query.Count();

            var pagedProducts = query
                .OrderBy(p => p.ProductName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ItemVM
                {
                    productID = p.ProductId,
                    productName = p.ProductName,
                    price = p.UnitPrice ?? 0,
                    image = p.Image ?? "",
                    unitDecription = p.UnitDescription,
                    categoryName = p.Category.CategoryName,
                    alias = p.Alias
                })
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.SelectedCategory = category;

            return View(pagedProducts);
        }

        public IActionResult Detail(int id)
        {
            // Truy vấn chi tiết sản phẩm và bao gồm danh mục
            var data = db.Products
                .Include(p => p.Category)
                .SingleOrDefault(p => p.ProductId == id);

            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            // Tạo ProductDetailVM và điền dữ liệu
            var result = new ProductDetailVM
            {
                productID = data.ProductId,
                productName = data.ProductName,
                price = data.UnitPrice ?? 0,
                image = data.Image ?? string.Empty,
                unitDescription = data.UnitDescription,
                description = data.Description,
                viewCount = data.ViewCount,
                // Lấy các sản phẩm liên quan (cùng danh mục, trừ sản phẩm hiện tại)
                RelatedProducts = db.Products
                    .Where(p => p.CategoryId == data.CategoryId && p.ProductId != id)
                    .Take(5) // Lấy tối đa 5 sản phẩm
                    .Select(p => new ItemVM
                    {
                        productID = p.ProductId, // Thêm productID để tạo liên kết động
                        productName = p.ProductName, // Sử dụng ProductName thay vì Name
                        image = p.Image ?? string.Empty,
                        price = p.UnitPrice ?? 0, // Sử dụng UnitPrice thay vì Price
                        alias = p.Alias
                    })
                    .ToList()
            };

            return View(result);
        }

    }
}
