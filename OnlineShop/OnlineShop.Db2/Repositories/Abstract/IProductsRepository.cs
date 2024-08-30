using OnlineShop.Db.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OnlineShop.Db.Repositories.Abstract
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();

       Task<Product> GetAsync(Guid id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Guid id);
    }
}
