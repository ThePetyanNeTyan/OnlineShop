using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShop.Db.Models;
using Microsoft.AspNet.Identity;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
	public class FavoritesViewComponent : ViewComponent
    {
		private readonly IProdcutsListRepository<Favorite> prodcutsListRepository;

		public FavoritesViewComponent(IProdcutsListRepository<Favorite> prodcutsListRepository)
		{
			this.prodcutsListRepository = prodcutsListRepository;
		}

		public IViewComponentResult Invoke()
        {

			if (User.Identity.IsAuthenticated)
			{
				var favorite = prodcutsListRepository.GetAsync(User.Identity.GetUserId()).Result;

				if (favorite == null) { return View("Favorites", 0); }

				var amount = favorite.Products.Count == 0 ? 0 : favorite.Products.Count;

				return View("Favorites", amount);
			}
            else
            {
                var favorite = HttpContext.Session.Get<Favorite>("Favorites");

                if (favorite == null) { return View("Favorites", 0); }

                var amount = favorite.Products.Count is 0 ? 0 : favorite.Products.Count;

                return View("Favorites", amount);
            }
        }
    }
}
