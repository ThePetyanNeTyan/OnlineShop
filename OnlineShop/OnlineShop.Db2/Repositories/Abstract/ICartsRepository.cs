using System;
using System.Collections.Generic;
using OnlineShop.Db.Models;
namespace OnlineShop.Db.Repositories.Abstract
{
    public interface ICartsRepository
    {
        Task AddAsync(Product product, string userId);

        Task AddCartAsync(Cart cart);

		List<Cart> GetAll();

        Task<Cart> GetAsync(string Id);

        Task DecreaseProductAsync(Guid productId, string cartId);

        Task ClearAsync( string userId);

    }
}