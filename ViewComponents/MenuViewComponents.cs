using Microsoft.AspNetCore.Mvc;
using TECHNOVA.Data;
using TECHNOVA.ViewModels;

namespace TECHNOVA.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly TechnovaContext db;
        public MenuViewComponent(TechnovaContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Categories.Select(c => new MenuVM
            {
                categoryID = c.CategoryId,
                categoryName = c.CategoryName,
            }).OrderBy(p => p.categoryName);

            return View(data); //Default.cshtml
            // return View("Default", data);
        }
    }
}
