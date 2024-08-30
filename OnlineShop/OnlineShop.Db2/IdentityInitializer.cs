using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;


namespace OnlineShop.Db
{
	public class IdentityInitializer
	{

		public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{

			var adminEmail = "admin@gmail.com";
			var password = "Aa123456!";

			if (roleManager.FindByNameAsync(RoleNames.AdminRoleName).Result == null)
			{
				roleManager.CreateAsync(new IdentityRole(RoleNames.AdminRoleName)).Wait();
			}

			if (roleManager.FindByNameAsync(RoleNames.AuthorizedUserRoleName).Result == null)
			{
				roleManager.CreateAsync(new IdentityRole(RoleNames.AuthorizedUserRoleName)).Wait();
			}

			if (roleManager.FindByNameAsync(RoleNames.AnonymousUserRoleName).Result == null)
			{
				roleManager.CreateAsync(new IdentityRole(RoleNames.AnonymousUserRoleName)).Wait();
			}

			if (userManager.FindByNameAsync(adminEmail).Result == null)
			{
				var admin = new User { Email = adminEmail, UserName = adminEmail, ImgPath = new List<string> { "/Images/UsersAvatar/default.png" } };
				var result = userManager.CreateAsync(admin, password).Result;
				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(admin,RoleNames.AdminRoleName).Wait();
				}
			}
		}
	}
}
