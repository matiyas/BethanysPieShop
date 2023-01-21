using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCart _shoppingCart;
        public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart) {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            // Get items from the db
            var items = _shoppingCart.GetShoppingCartItems();

            // Assign items to the local property
            _shoppingCart.ShoppingCartItems = items;
            
            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }
        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var pie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == pieId);


            if (pie != null)
            {
                _shoppingCart.AddToCart(pie);
            }

            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var pie = _pieRepository.GetPieById(pieId);

            if (pie != null)
            {
                _shoppingCart.RemoveFromCart(pie);
            }

            return RedirectToAction("Index");
        }
    }
}
