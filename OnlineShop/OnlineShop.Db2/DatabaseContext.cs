using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; } = null;

		public DbSet<CartProduct> CartProducts { get; set; }

		public DbSet<Product> Products { get; set; } 

		public DbSet<Comparison> Comparisons { get; set; } = null;

		public DbSet<Favorite> Favorites { get; set; } = null;

		public DbSet<Order> Orders { get; set; } = null;

		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
			Database.Migrate();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		
			modelBuilder.Entity<Cart>(b =>
			{
				b.HasMany(p => p.CartProducts)
				.WithOne(p => p.Cart);
			});

			modelBuilder.Entity<Product>(b =>
			{
				b.HasMany(p => p.CartProducts)
				.WithOne(p => p.Product);
			});

			modelBuilder.Entity<Order>()
				.HasOne(p => p.OrderDetails)
				.WithOne(p => p.Order)
				.HasPrincipalKey<Order>(p=>p.Id)
                .HasForeignKey<OrderDetails>(p => p.OrderId);


            modelBuilder.Entity<Product>(x =>
            {

                x.Property(y => y.ImgPath)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });




            modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Маргарита",
					Cost = 760,
					Description = "С сыром пармезан и помидорами черри",
					ImgPath = new List<string> { "/Images/Products/margarita.jpg" },
					Bonus = 55
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Цезарь",
					Cost = 780,
					Description = "С сыром пармезан и листьями салата",
					ImgPath = new List<string> { "/Images/Products/cezar.jpg" },
					Bonus = 60
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "Грибная",
					Cost = 770,
					Description = "С шампиньонами с картошкой",
					ImgPath = new List<string> { "/Images/Products/gribnaya.jpg" },
					Bonus = 55
				}


			);
		}
	}
}
