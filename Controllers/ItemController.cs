using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                })
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.SelectedCategory = category;

            return View(pagedProducts);
        }

    }
}
