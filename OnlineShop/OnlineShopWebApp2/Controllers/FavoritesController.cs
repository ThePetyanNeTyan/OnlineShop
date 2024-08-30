using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShop.Db.Models;
using Microsoft.AspNet.Identity;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Controllers
{

    public class FavoritesController : Controller
    {
        private readonly IProdcutsListRepository<Favorite> prodcutsListRepository;

        private readonly IProductsRepository productsRepository;

        private readonly IService<Favorite> favoriteService;

        public FavoritesController(IProdcutsListRepository<Favorite> prodcutsListRepository, IProductsRepository productsRepository, IService<Favorite> favoriteService)
        {
            this.prodcutsListRepository = prodcutsListRepository;
            this.productsRepository = productsRepository;
            this.favoriteService = favoriteService;
        }

        public IActionResult Favorites()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                var favorite = prodcutsListRepository.GetAsync(userId).Result;

                if (favorite == null) return View(new Favorite()); 

                return View(favorite);
            }

            else
            {
                var favorite = HttpContext.Session.Get<Favorite>("Favorites");

                if (favorite == null)  return View(new Favorite());

                return View(favorite);
            }
        }

        public async Task<IActionResult> Add( Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                await prodcutsListRepository.AddAsync(productId, userId);

                return Redirect("/Home/Index");
            }

            else
            {
                var favorite = HttpContext.Session.Get<Favorite>("Favorites");

                var product = productsRepository.GetAsync(productId).Result;

                favorite=favoriteService.Add(product, favorite);

                HttpContext.Session.Set<Favorite>("Favorites", favorite);

                return Redirect("/Home/Index");
            }
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            if (User.Identity.IsAuthenticated)
            {

                var userId = User.Identity.GetUserId();

                var favoriteId = prodcutsListRepository.GetAsync(userId).Result.Id;

                await prodcutsListRepository.DeleteAsync(favoriteId, productId);

                return RedirectToAction("Favorites");
            }

            else
            {
                var favorite = HttpContext.Session.Get<Favorite>("Favorites");

                favorite= favoriteService.Remove(productId, favorite);

                HttpContext.Session.Set<Favorite>("Favorites", favorite);

                if (favorite.Products.Count == 0) favorite.Products.Clear();

                return RedirectToAction("Favorites");
            }
        }
        public async Task<IActionResult> Clear()
        {
            if (User.Identity.IsAuthenticated) await prodcutsListRepository.ClearAsync(User.Identity.GetUserId());

            else HttpContext.Session.Remove("Favorites");

            return RedirectToAction("Favorites");
        }

    }
}
