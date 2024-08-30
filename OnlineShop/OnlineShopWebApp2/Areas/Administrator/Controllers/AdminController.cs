using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
	[Area(RoleNames.AdminRoleName)]
	[Authorize(Roles = RoleNames.AdminRoleName)]
	public class AdminController : Controller
	{
		private readonly IProductsRepository productsRepository;

		private readonly IOrdersRepository repositoryOrder;

		private readonly UserManager<User> userManager;

		private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(IProductsRepository repositoryProducts, IOrdersRepository repositoryOrder, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.productsRepository = repositoryProducts;
            this.repositoryOrder = repositoryOrder;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Orders()
		{
			var orders = await repositoryOrder.GetAllAsync();

			return View(orders);
		}

		public IActionResult Users()
		{
			var users = userManager.Users.ToList();

			return View(users);
		}

		public IActionResult Roles()
		{
			var roles = roleManager.Roles.ToList();

            return View(roles.Select(p=>new RoleViewModel { Name = p.Name }).ToList());
		}

		public async Task<IActionResult> Products()
		{
			var productsBD = await productsRepository.GetAllAsync();

			return View(productsBD);
		}
	}
}

