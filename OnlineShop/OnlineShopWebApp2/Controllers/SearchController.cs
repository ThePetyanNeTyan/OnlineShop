using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Abstract;


namespace OnlineShopWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public SearchController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult NotFoundProduct() => View();
       

        [HttpPost]
        public async Task<IActionResult> Search(string productName)
        {
            var products =await productsRepository.GetAllAsync();
                                           
            if(products.Any(p=>p.Name==productName))
            {
             var product= products.FirstOrDefault(p=>p.Name==productName);

              return View(product);
            }
            
            else return RedirectToAction("NotFoundProduct");
        } 

    } 
}
