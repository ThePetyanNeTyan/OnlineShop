using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;


namespace OnlineShop.Db.Repositories
{
    public class ComaprisonsDbRepository: IProdcutsListRepository<Comparison>
    {
        private readonly  IProductsRepository productsRepository;

		private readonly DatabaseContext databaseContext;

		public ComaprisonsDbRepository(IProductsRepository productsRepository, DatabaseContext databaseContext)
        {
            this.productsRepository = productsRepository;

			this.databaseContext = databaseContext;
		}

        public async Task AddAsync(Comparison comprison)
        {
            var newComparison = new Comparison { Id = comprison.Id };

            for (int i = 0; i <= comprison.Products.Count - 1; i++)
            {
                newComparison.Products.Add( await productsRepository.GetAsync(comprison.Products[i].Id));
            }

            await databaseContext.Comparisons.AddAsync(newComparison);

            await databaseContext.SaveChangesAsync();
        }
        public async Task AddAsync(Guid productId,string userId)
        {
            var comparison = await GetAsync(userId);

            var product= await productsRepository.GetAsync(productId);

            if (comparison == null)
            {
                comparison = new Comparison { Id = userId, Products = new() { product } };

                databaseContext.Comparisons.Add(comparison);
            }

			if (!comparison.Products.Any(p=>p.Id==productId)) comparison.Products.Add(product);

            await  databaseContext.SaveChangesAsync();
        }

        public  async Task<Comparison> GetAsync(string id)=>await databaseContext.Comparisons.Include(x => x.Products).FirstOrDefaultAsync(p => p.Id == id) ?? null;

        public async Task DeleteAsync(string comparisonId,Guid productId)
        {
            var comparison = await GetAsync(comparisonId);

            var product =  comparison.Products.FirstOrDefault(p => p.Id == productId);

            comparison.Products.Remove(product);

            await databaseContext.SaveChangesAsync();

            if (comparison.Products.Count == 0) await ClearAsync(comparisonId);

            await  databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string id)
        {
			var comparison = await GetAsync(id);

            databaseContext?.Comparisons.Remove(comparison);

            await databaseContext.SaveChangesAsync();
		}
    }
}
