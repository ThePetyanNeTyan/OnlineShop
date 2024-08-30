using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShop.Db.Repositories.Abstract;
using System;
using Microsoft.AspNet.Identity;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent:ViewComponent
    {
        private readonly ICartsRepository repositoryCart;

        public CartViewComponent(ICartsRepository repositoryCart)
        {
            this.repositoryCart = repositoryCart;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cartBD = repositoryCart.GetAsync(User.Identity.GetUserId()).Result;

                if (cartBD == null)return View("Cart", 0);

                var cartVM = MapperHelper.Mapper.Map<CartViewModel>(cartBD);

                var productCount = cartVM?.AmountProducts is 0 ? 0 : cartVM?.AmountProducts;

                return View("Cart", productCount);
            }

            else
            {
                var cartMC = HttpContext.Session.Get<OnlineShop.Db.Models.Cart>("Cart");

                var cartVM = MapperHelper.Mapper.Map<CartViewModel>(cartMC);

                var productCount = cartVM?.AmountProducts is 0 ? 0 : cartVM?.AmountProducts;

                return View("Cart", productCount);
            }
        }
        

        }
    }

