using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.UserAvatar
{
    public class UserAvatarViewComponent : ViewComponent
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public UserAvatarViewComponent(Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var email = User.Identity.GetUserName().ToString();

            var userDB=userManager.FindByEmailAsync(email).Result;

            var userVM = MapperHelper.Mapper.Map<UserViewModel>(userDB);

            return View("UserAvatar",userVM);
        }
    }
}
