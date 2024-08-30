
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;


namespace OnlineShop.Db.Repositories
{
	public class CartsDbRepository : ICartsRepository
	{

		private readonly DatabaseContext databaseContext;

		private readonly IProductsRepository productsRepository;

        public CartsDbRepository(DatabaseContext databaseContext, IProductsRepository productsRepository)
        {
            this.databaseContext = databaseContext;

            this.productsRepository = productsRepository;
        }

        public async Task AddCartAsync(Cart cart)
		{
            var newCart = new Cart() { Id = cart.Id };

            for (int i = 0; i <= cart.CartProducts.Count - 1; i++)
            {
                var cartProduct = new CartProduct { Product = await productsRepository.GetAsync(cart.CartProducts[i].Product.Id), Amount = cart.CartProducts[i].Amount };

                newCart.CartProducts.Add(cartProduct);
            }
            
            await databaseContext.Carts.AddAsync(newCart);

            await databaseContext.SaveChangesAsync();
        }

		public async Task AddAsync(Product product, string userId)
		{
			if (userId != null)
			{
				var cart = await GetAsync(userId);

				if (product == null) return;

				else
				{
					var cartProduct = new CartProduct { Product = product, Amount = 1 };

					if (cart == null)
					{
                        cart = new Cart() { Id = userId };

						cart.CartProducts.Add(cartProduct);

						await databaseContext.Carts.AddAsync(cart);
					}

					else
					{
						if (!cart.CartProducts.Any(p => p.Product.Id == product.Id)) cart.CartProducts.Add(cartProduct);

						else
						{
                            cart.CartProducts.FirstOrDefault(p => p.Product.Id == product.Id).Amount += 1;
						}
					}
				}
				await databaseContext.SaveChangesAsync();
			}
		
		}

		public List<Cart> GetAll() => databaseContext.Carts.ToList();

		public async Task<Cart> GetAsync(string id) => await databaseContext.Carts.Include(p => p.CartProducts).ThenInclude(p => p.Product).FirstOrDefaultAsync(x => x.Id == id) ?? null;

		public async Task DecreaseProductAsync(Guid productId, string cartId)
		{
			var cart = await GetAsync(cartId);

			var product = cart.CartProducts.FirstOrDefault(p => p.Product.Id == productId);

			product.Amount -= 1;

			if (product.Amount == 0) await	ClearAsync(cartId);

			await databaseContext.SaveChangesAsync();

			if (cart.CartProducts.Count==0) await ClearAsync(cartId);

			await databaseContext.SaveChangesAsync();
		}

		public async Task ClearAsync(string id)
		{
			var cart = GetAsync(id).Result;

			databaseContext.Carts.Remove(cart);

			await databaseContext.SaveChangesAsync();
		}
	}
}

