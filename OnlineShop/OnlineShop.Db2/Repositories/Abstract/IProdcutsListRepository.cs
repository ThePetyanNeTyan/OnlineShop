using System;
namespace OnlineShop.Db.Repositories.Abstract
{
    public interface IProdcutsListRepository<T> where T : class
    {
        Task AddAsync(Guid productId, string userId);

        Task AddAsync(T obj);

        Task<T> GetAsync(string id);

        Task ClearAsync(string id);

        Task DeleteAsync(string comparisonId, Guid productId);
    }
}
