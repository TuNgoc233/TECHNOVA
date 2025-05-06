using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.ViewModels;


namespace TECHNOVA.ViewComponents
{
    public class SupplierListViewComponent : ViewComponent
    {
        private readonly TechnovaContext _context;

        public SupplierListViewComponent(TechnovaContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var suppliers = _context.Suppliers
                .Select(s => new SupplierVM
                {
                    SupplierId = s.SupplierId, // Hoặc tùy thuộc vào thuộc tính của bạn
                    SupplierName = s.SupplierName,
                    ProductCount = s.Products.Count()
                })
                .OrderByDescending(s => s.ProductCount)
                .Take(5) // Lấy 5 nhà cung cấp có nhiều sản phẩm nhất
                .ToList();

            return View(suppliers); // Trả về danh sách 5 nhà cung cấp
        }


    }

}