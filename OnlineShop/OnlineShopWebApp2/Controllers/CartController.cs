using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
	{
		private readonly ICartsRepository cartsRepository;

		private readonly IProductsRepository productsRepository;

        private readonly IService<Cart> cartService;


        public CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository, IService<Cart> cartService)
        {
            this.cartsRepository = cartsRepository;
            this.productsRepository = productsRepository;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var userId = User.Identity.GetUserId();

				var cartBD = await cartsRepository.GetAsync(userId);

				var cartVM = MapperHelper.Mapper.Map<CartViewModel>(cartBD);

				return View(cartVM);
			}

			else
			{
                var cart = HttpContext.Session.Get<Cart>("Cart");

                var cartVM = MapperHelper.Mapper.Map<CartViewModel>(cart);

                return View(cartVM);
            }
		}
		

		public async Task<IActionResult> Add(Guid productId)
		{
            if (User.Identity.IsAuthenticated)
			{
				var product = await productsRepository.GetAsync(productId);

				var userId = User.Identity.GetUserId();

				await cartsRepository.AddAsync(product, userId);
			}
            else
            {
                var cart = HttpContext.Session.Get<Cart>("Cart");

                var product = productsRepository.GetAsync(productId).Result;

                cart=cartService.Add(product,cart);

                HttpContext.Session.Set<Cart>("Cart", cart);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

		public async Task<IActionResult> DecreaseProduct(Guid productId, string cartId)
		{
			if (User.Identity.IsAuthenticated)
			{
				await cartsRepository.DecreaseProductAsync(productId, cartId);
            }

			else
			{
                var cart = HttpContext.Session.Get<Cart>("Cart");

                cart=cartService.Remove(productId,cart);

                HttpContext.Session.Set<Cart>("Cart", cart);
            }

            return RedirectToAction("Index");
        }

		public async Task<IActionResult> Clear(string? id)
		{
            if (User.Identity.IsAuthenticated) await cartsRepository.ClearAsync(id);

            else HttpContext.Session.Remove("Cart");

            return View("Index");
		}
	}
}









