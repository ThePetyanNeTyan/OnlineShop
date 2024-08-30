using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShop.Db;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Identity;
using OnlineShopWebApp.Helpers;



namespace OnlineShopWebApp.Areas.Admin.Controllers
{
	[Area(RoleNames.AdminRoleName)]
	[Authorize(Roles = RoleNames.AdminRoleName)]
	public class UserController : Controller
	{
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }


        public IActionResult CreateForm()
		{
			return View(new Registration());
		}


		[HttpPost]
		public async Task<IActionResult> Add(Registration registration)
		{
            if (ModelState.IsValid)
            {
                var user = new User { Email = registration.Email, UserName = registration.Name, PhoneNumber = registration.Phone,Age=registration.Age };

                var result = await userManager.CreateAsync(user, registration.PasswordUser);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,RoleNames.AuthorizedUserRoleName);

                    return RedirectToAction("Users", "Admin");
                }
            }

			return View("CreateForm", registration);
		}


        public async Task<IActionResult> UserInform(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return View(user);
        }


        public IActionResult EditPasswordForm(string email)
        {
            ViewBag.Email = email;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePassword(Registration registration, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var _passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;

                var _passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                IdentityResult result = await _passwordValidator.ValidateAsync(userManager, user, registration.PasswordUser);
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, registration.PasswordUser);
                    await userManager.UpdateAsync(user);
                    return RedirectToAction("Users", "Admin");
                }
            }

            return RedirectToAction("EditPasswordForm");
        }


        public async Task<IActionResult> EditDataForm(string email)
        {
            var userDB = await userManager.FindByEmailAsync(email);

            var userVM=MapperHelper.Mapper.Map<UserViewModel>(userDB);

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var userDB = await userManager.FindByEmailAsync(userVM.Email);
                if (userDB != null)
                {
                    userDB.Age = userVM.Age;
                    userDB.Email = userVM.Email;
                    userDB.UserName = userVM.UserName;
                    userDB.PhoneNumber= userVM.PhoneNumber;
                }

                await userManager.UpdateAsync(userDB);

                return RedirectToAction("Users", "Admin");
            }

            return View("EditDataForm", userVM.Email);
        }

        public async Task<IActionResult> Delete(string email)
        {
            var user= await userManager.FindByEmailAsync(email);

            await userManager.DeleteAsync(user);

            return RedirectToAction("Users", "Admin");

        }

    }
}