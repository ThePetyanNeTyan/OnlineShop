
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;


namespace OnlineShopWebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		
		private readonly SignInManager<User> signInManager;

		private readonly string FILE_PATH = "/Images/UsersAvatar/default.png";

        public AccountController(SignInManager<User> signInManager,UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login(string returnUrl)
		{
			return View(new Login { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> Authorization(Login login)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberUser,false);

                if (result.Succeeded)
				{
					return Redirect(login.ReturnUrl ?? "/Home");
				}

				else ModelState.AddModelError("", "Неправильный пароль");
			}

			return View("Login", login);
		}

		public IActionResult RegistrationForm(string returnUrl)
		{
			return View(new Registration { ReturnUrl=returnUrl});
		}


		[HttpPost]
		public async Task<IActionResult> Registration(Registration registration)
		{
			if (ModelState.IsValid)
			{
				
					var user = new User { Email = registration.Email, UserName = registration.Email, PhoneNumber = registration.Phone, Age = registration.Age,ImgPath=new List<string> { FILE_PATH } };

					var result = await userManager.CreateAsync(user, registration.PasswordUser);

					if (result.Succeeded)
					{
						await signInManager.SignInAsync(user, false);

						await userManager.AddToRoleAsync(user,RoleNames.AuthorizedUserRoleName);

                    return Redirect(registration.ReturnUrl ?? "/Home/SessionData");
					}
				}

			return View("RegistrationForm", registration);
		}

		public async Task<IActionResult> LogOut()
		{
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


	}
}
