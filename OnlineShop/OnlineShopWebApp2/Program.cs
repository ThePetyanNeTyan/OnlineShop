using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShop.Db.Repositories.Abstract;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
	loggerConfiguration
		.ReadFrom.Configuration(hostingContext.Configuration)
		.Enrich.FromLogContext()
		.Enrich.WithProperty("ApplicationName", "Online Shop");
});

string connection = builder.Configuration.GetConnectionString("online_shop");

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<DatabaseContext>(options =>
	  options.UseSqlServer(connection));

builder.Services.AddDbContext<IdentityContext>(options =>
	  options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/LogOut";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
builder.Services.AddTransient<IProdcutsListRepository<Comparison>, ComaprisonsDbRepository>();
builder.Services.AddTransient<IProdcutsListRepository<Favorite>, FavoritesDbRepository>();
builder.Services.AddSingleton<ImageHelper>();
builder.Services.AddSingleton<MapperHelper>();
builder.Services.AddServices();
builder.Services.AddRazorPages();
MapperHelper.CreateMapper();





var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
		name: "MyArea",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();



