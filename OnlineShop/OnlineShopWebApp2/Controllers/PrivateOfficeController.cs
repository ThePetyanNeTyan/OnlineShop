using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class PrivateOfficeController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        private readonly ImageHelper imageHelper;

        private readonly string FILE_PATH = "/Images/UsersAvatar/default.png";

        private readonly IOrdersRepository ordersRepository;

        public PrivateOfficeController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, ImageHelper imageHelper, IOrdersRepository ordersRepository)
        {
            this.userManager = userManager;
            this.imageHelper = imageHelper;
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var email = User.Identity.GetUserName().ToString();

            var userDB = userManager.FindByEmailAsync(email).Result;

            var userVM = MapperHelper.Mapper.Map<UserViewModel>(userDB);

            ViewBag.Orders=ordersRepository.GetAllUserOrdersAsync(User.Identity.GetUserId());

            return View(userVM);
        }


        public async Task<IActionResult> EditForm(string email)
        {
            var userDB = await userManager.FindByEmailAsync(email);

            var userVM = MapperHelper.Mapper.Map<UserViewModel>(userDB);

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userVM)
        {
            userVM.ImgPath = imageHelper.Edit(userVM, "UsersAvatar");

            var userDB = await userManager.FindByEmailAsync(userVM.Email);
           
            if (userDB != null)
            {
                userDB.Age = userVM.Age;
                userDB.Email = userVM.Email;
                userDB.UserName = userVM.UserName;
                userDB.PhoneNumber = userVM.PhoneNumber;
                userDB.ImgPath = userVM.ImgPath;
            }

            await userManager.UpdateAsync(userDB);

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> Delete(string img,string email)
        {
            var userDB = await userManager.FindByEmailAsync(email);

            userDB.ImgPath.Remove(img);

            if(userDB.ImgPath.Count==0)userDB.ImgPath.Add(FILE_PATH);

            await userManager.UpdateAsync(userDB);

            return Redirect("Index");
        }
    }
}
