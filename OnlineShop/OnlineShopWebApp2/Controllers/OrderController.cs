using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;


namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;

        private readonly ICartsRepository cartRepository;

        public OrderController(IOrdersRepository ordersRepository, ICartsRepository cartRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartRepository = cartRepository;
        }

        public async Task<IActionResult> OrderForm()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart");

                if (cart != null)
                {
                    cart.Id = User.Identity.GetUserId();

                    await cartRepository.AddCartAsync(cart);

                }
                var orderDetailsVM = new OrderDetailsViewModel();

                return View(orderDetailsVM);
            }

            else return RedirectToAction("RegistrationForm", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Save(OrderDetailsViewModel orderDetailsVM)
        {
			if (ModelState.IsValid)
            { 
                var orderVM = new OrderViewModel();

                orderVM.OrderDetails = orderDetailsVM;

				orderVM.UserId = User.Identity.GetUserId();

			    orderVM.OrderDetails.EmailUser = User.Identity.GetUserName();

				var orderBD = MapperHelper.Mapper.Map<Order>(orderVM);

                orderBD.CartProducts= cartRepository.GetAsync(orderVM.UserId).Result.CartProducts;

				await ordersRepository.SaveAsync(orderBD);

                await cartRepository.ClearAsync(orderBD.UserId);

                return View();
            }

            return View("OrderForm", orderDetailsVM);
        }

    }
}
