using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

		public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Product>> GetAllAsync() => await databaseContext.Products.ToListAsync();

		public async Task<Product> GetAsync(Guid id) => await databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product)
        {
          
			await databaseContext.Products.AddAsync(product);
         
           await databaseContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

			 databaseContext.Products.Remove(product);

           await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
           await DeleteAsync(product.Id);

			await databaseContext.Products.AddAsync(product);

            await databaseContext.SaveChangesAsync();
		}
    }
}
