using EcomQLDM.Data;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcomQLDM.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        private readonly TmdtdatabaseContext db;

        public MenuCategoryViewComponent(TmdtdatabaseContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuCategoryVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            return View(data); // Default.cshtml
            //return View("Default", data);
        }
    }
}
