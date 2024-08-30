using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class FavoritesDbRepository : IProdcutsListRepository<Favorite>
    {
		private readonly IProductsRepository productsRepository;

		private readonly DatabaseContext databaseContext;

		public FavoritesDbRepository(IProductsRepository productsRepository, DatabaseContext databaseContext)
		{
			this.productsRepository = productsRepository;

			this.databaseContext = databaseContext;
		}

        public async Task AddAsync(Favorite favorite)
        {
			var newFavorite = new Favorite { Id = favorite.Id};

            for (int i = 0; i <= favorite.Products.Count - 1; i++)
			{
				newFavorite.Products.Add(await productsRepository.GetAsync(favorite.Products[i].Id));
			}

            await databaseContext.Favorites.AddAsync(newFavorite);

            await databaseContext.SaveChangesAsync();
        }

        public async Task AddAsync(Guid productId, string userId)
		{
			var favorite = await GetAsync(userId);
		
            var product = await productsRepository.GetAsync(productId);

			if (favorite == null)
			{
				favorite = new Favorite { Id = userId, Products = new() { product } };

				await databaseContext.Favorites.AddAsync(favorite);
			}

			if (!favorite.Products.Any(p => p.Id == productId)) favorite.Products.Add(product);

			await databaseContext.SaveChangesAsync();
		}

		public async Task<Favorite> GetAsync(string id)=>await databaseContext.Favorites.Include(x => x.Products).FirstOrDefaultAsync(p => p.Id == id) ?? null;
		

		public async Task DeleteAsync(string favoriteId, Guid productId)
		{
			var favorite = await GetAsync(favoriteId);

			var product = favorite.Products.FirstOrDefault(p => p.Id == productId);

			favorite.Products.Remove(product);

			await databaseContext.SaveChangesAsync();

			if (favorite.Products.Count == 0) await ClearAsync(favoriteId);

			await databaseContext.SaveChangesAsync();
		}

		public async Task ClearAsync(string id)
		{
			var favorite = await GetAsync(id);

			databaseContext?.Favorites.Remove(favorite);

			await databaseContext.SaveChangesAsync();
		}
	}
}
