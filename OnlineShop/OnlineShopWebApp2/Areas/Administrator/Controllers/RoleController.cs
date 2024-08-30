using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;


namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(RoleNames.AdminRoleName)]
    [Authorize(Roles = RoleNames.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController( RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult CreateForm()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (await roleManager.RoleExistsAsync(role.Name))

                    if (!await roleManager.RoleExistsAsync(role.Name)) ModelState.AddModelError("Name", "Такая роль уже существует");
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role.Name});

                    return RedirectToAction("Roles", "Admin");

                }
            }

            return View("CreateForm", role);
        }


        public async Task<IActionResult> Delete(string name)
        {
            var role = await roleManager.FindByNameAsync(name);

            await roleManager.DeleteAsync(role);

            return RedirectToAction("Roles", "Admin");

        }
    }
}
