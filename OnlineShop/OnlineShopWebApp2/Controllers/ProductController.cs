using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductsRepository productsRepository;

		public ProductController(IProductsRepository productsRepository)
		{
			this.productsRepository = productsRepository;
		}

		public IActionResult Index(Guid id)
		{
			var productBD = productsRepository.GetAsync(id).Result;

			var productVM = MapperHelper.Mapper.Map<Product, ProductViewModel>(productBD);

			return View(productVM);
		}
	}
}
