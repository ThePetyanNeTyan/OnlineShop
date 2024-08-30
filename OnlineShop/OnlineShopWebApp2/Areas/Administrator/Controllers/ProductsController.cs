using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;


namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(RoleNames.AdminRoleName)]
    [Authorize(Roles = RoleNames.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository repositoryProducts;

        private readonly ImageHelper imageHelper;


        public ProductsController(IProductsRepository repositoryProducts, ImageHelper loadImage)
        {
            this.repositoryProducts = repositoryProducts;
            this.imageHelper = loadImage;
        }

        public IActionResult CreateForm()
        {
            var product = new ProductViewModel();

			return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                productVM.ImgPath = imageHelper.Load(productVM,"Products");

                var productBD = MapperHelper.Mapper.Map<Product>(productVM);
			
				await repositoryProducts.AddAsync(productBD);

                return RedirectToAction("Products", "Admin");
            }
            return View("CreateForm", productVM);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await repositoryProducts.DeleteAsync(id);

            return RedirectToAction("Products", "Admin");
        }

        public IActionResult EditForm(Guid id)
        {
            var productBD = repositoryProducts.GetAsync(id).Result;

			var productVM = MapperHelper.Mapper.Map<ProductViewModel>(productBD);

			return View(productVM);

        }

        [HttpPost]
        public async Task<IActionResult> Modified(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
			{
                productVM.ImgPath =imageHelper.Edit(productVM, "Products");

				var productDb = MapperHelper.Mapper.Map<Product>(productVM);
			
				await repositoryProducts.UpdateAsync(productDb);

                return RedirectToAction("Products", "Admin");
            }

            return View("CreateForm", productVM);
        }
    }
}
