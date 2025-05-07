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

        public IActionResult Index(int? categoryId, int page = 1)
        {
            int pageSize = 9;

            var query = db.Products.AsNoTracking().AsQueryable();

            if (categoryId.HasValue)
            {
                // Kiểm tra danh mục có tồn tại
                if (!db.Categories.Any(c => c.CategoryId == categoryId.Value))
                {
                    TempData["Message"] = "Danh mục không hợp lệ.";
                    return RedirectToAction("Index");
                }
                query = query.Where(p => p.CategoryId == categoryId.Value);
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
                    //unitDescription = p.UnitDescription, // Sửa lỗi chính tả
                    categoryName = p.Category.CategoryName,
                    alias = p.Alias
                })
                .ToList();

            // Lấy danh sách danh mục cho sidebar
            var categories = db.Categories
                .AsNoTracking()
                .Select(c => new MenuVM
                {
                    categoryID = c.CategoryId,
                    categoryName = c.CategoryName
                })
                .ToList();

            ViewData["Categories"] = categories;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.SelectedCategory = categoryId;

            return View(pagedProducts);
        }

        public IActionResult Detail(int id)
        {
            var data = db.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .SingleOrDefault(p => p.ProductId == id);

            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            var result = new ProductDetailVM
            {
                productID = data.ProductId,
                productName = data.ProductName,
                price = data.UnitPrice ?? 0,
                image = data.Image ?? string.Empty,
                unitDescription = data.UnitDescription,
                description = data.Description,
                viewCount = data.ViewCount,
                RelatedProducts = db.Products
                    .AsNoTracking()
                    .Where(p => p.CategoryId == data.CategoryId && p.ProductId != id)
                    .Take(5)
                    .Select(p => new ItemVM
                    {
                        productID = p.ProductId,
                        productName = p.ProductName,
                        image = p.Image ?? string.Empty,
                        price = p.UnitPrice ?? 0,
                        alias = p.Alias
                    })
                    .ToList()
            };

            ViewData["Categories"] = db.Categories
                .AsNoTracking()
                .Select(c => new MenuVM
                {
                    categoryID = c.CategoryId,
                    categoryName = c.CategoryName
                })
                .ToList();

            return View(result);
        }

    }
}
