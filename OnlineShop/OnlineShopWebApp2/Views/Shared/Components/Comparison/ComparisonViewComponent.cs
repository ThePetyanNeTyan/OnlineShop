using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Comparison
{
    public class ComparisonViewComponent : ViewComponent
    {
        private readonly IProdcutsListRepository<OnlineShop.Db.Models.Comparison> prodcutsListRepository;

        public ComparisonViewComponent(IProdcutsListRepository<OnlineShop.Db.Models.Comparison> prodcutsListRepository)
        {
            this.prodcutsListRepository = prodcutsListRepository;
        }
        public IViewComponentResult Invoke()
        {

            if (User.Identity.IsAuthenticated)
            {
                var comparison = prodcutsListRepository.GetAsync(User.Identity.GetUserId()).Result;

                if (comparison == null) { return View("Comparison", 0); }

                var amount = comparison.Products.Count == 0 ? 0 : comparison.Products.Count;

                return View("Comparison", amount);
            }
            else
            {
                var comparison = HttpContext.Session.Get<OnlineShop.Db.Models.Comparison>("Comparison");

                if (comparison == null) { return View("Comparison", 0); }

                var amount = comparison.Products.Count is 0 ? 0 : comparison.Products.Count;

                return View("Comparison", amount);
            }
        }
    }
}
