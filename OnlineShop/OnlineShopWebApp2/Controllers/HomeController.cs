using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;

        private readonly ICartsRepository cartRepository;

        private readonly IProdcutsListRepository<Favorite> favoritesRepository;

        private readonly IProdcutsListRepository<Comparison> comparisonRepository;


        public HomeController(IProductsRepository productsRepository, ICartsRepository cartRepository, IProdcutsListRepository<Favorite> favoritesRepository, IProdcutsListRepository<Comparison> comparisonRepository)
        {
            this.productsRepository = productsRepository;
            this.cartRepository = cartRepository;
            this.favoritesRepository = favoritesRepository;
            this.comparisonRepository = comparisonRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productsBD = await productsRepository.GetAllAsync();

            var productsVM = MapperHelper.Mapper.Map<List<ProductViewModel>>(productsBD);

            return View(productsVM);
        }

        public async Task<IActionResult> SessionData()
        {

            if (User.Identity.IsAuthenticated)
            {
                var Id = User.Identity.GetUserId();

                var cart = HttpContext.Session.Get<Cart>("Cart");

                var favorite = HttpContext.Session.Get<Favorite>("Favorites");

                var comparison = HttpContext.Session.Get<Comparison>("Comparison");

                if (cart != null)
                {
                    cart.Id = Id;

                    await cartRepository.AddCartAsync(cart);
                }

                if (favorite != null)
                {
                    favorite.Id = Id;

                    await favoritesRepository.AddAsync(favorite);
                }
                if(comparison!= null)
                {
                    comparison.Id = Id;

                    await comparisonRepository.AddAsync(comparison);
                }

            }

            return RedirectToAction("Index");

        }



    }
}
