using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Db;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(RoleNames.AdminRoleName)]
    [Authorize(Roles = RoleNames.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository repositoryOrder;

        public OrderController(IOrdersRepository repositoryOrder)
        {
            this.repositoryOrder = repositoryOrder;
        }
        public async Task<IActionResult> EditInform(Guid id)
        {
            var orderBD = await repositoryOrder.GetAsync(id);

            var orderVM = MapperHelper.Mapper.Map<OrderViewModel>(orderBD);

			return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(Guid id, OrderStatus status)
        {
            await repositoryOrder.EditStatusAsync(id, status);

            return RedirectToAction("Orders", "Admin");
        }
    }
}
