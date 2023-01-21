using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        public readonly IPieRepository _pieRepository;
        public readonly ICategoryRepository _categoryRepository;

        public PieController (IPieRepository pierRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pierRepository;
            _categoryRepository = categoryRepository;
        }

        //public IActionResult List()
        //{
        //    var piesListViewModel = new PieListViewModel(_pieRepository.AllPies, "All pies");
        //    return View(piesListViewModel);
        //}

        public IActionResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(pie => pie.Name);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(pie => pie.Category.CategoryName == category).OrderBy(pie => pie.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
