using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Controllers
{

    public class ComparisonController : Controller
    {
		private readonly IProdcutsListRepository<Comparison> prodcutsListRepository;

        private readonly IProductsRepository productsRepository;

        private readonly IService<Comparison> comparisonService;

        public ComparisonController(IProdcutsListRepository<Comparison> prodcutsListRepository, IService<Comparison> comparisonService, IProductsRepository productsRepository)
        {
            this.prodcutsListRepository = prodcutsListRepository;
            this.comparisonService = comparisonService;
            this.productsRepository = productsRepository;
        }

        public IActionResult Comparison()
		{
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                var comparison = prodcutsListRepository.GetAsync(userId).Result;

                if (comparison == null) return View(new Comparison());

                return View(comparison);
            }

            else
            {
                var comprison = HttpContext.Session.Get<Comparison>("Comparison");

                if (comprison == null) return View(new Comparison());

                return View(comprison);
            }
        }

		public async Task<IActionResult> Add(Guid productId)
		{
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                await prodcutsListRepository.AddAsync(productId, userId);

                return Redirect("/Home/Index");
            }

            else
            {
                var coparison = HttpContext.Session.Get<Comparison>("Comparison");

                var product = productsRepository.GetAsync(productId).Result;

                coparison = comparisonService.Add(product, coparison);

                HttpContext.Session.Set<Comparison>("Comparison", coparison);

                return Redirect("/Home/Index");
            }
        }

		public async Task<IActionResult> Remove(Guid productId)
		{
            if (User.Identity.IsAuthenticated)
            {

                var userId = User.Identity.GetUserId();

                var comparisonId = prodcutsListRepository.GetAsync(userId).Result.Id;

                await prodcutsListRepository.DeleteAsync(comparisonId, productId);

                return RedirectToAction("Comparison");
            }

            else
            {
                var comparison = HttpContext.Session.Get<Comparison>("Comparison");

                comparison = comparisonService.Remove(productId, comparison);

                HttpContext.Session.Set<Comparison>("Comparison", comparison);

                if (comparison.Products.Count == 0) comparison.Products.Clear();

                return RedirectToAction("Comparison");
            }
        }
		public async Task<IActionResult> Clear()
		{
            if (User.Identity.IsAuthenticated) await prodcutsListRepository.ClearAsync(User.Identity.GetUserId());

            else HttpContext.Session.Remove("Comparison");

            return RedirectToAction("Comparison");
        }

	}
}
