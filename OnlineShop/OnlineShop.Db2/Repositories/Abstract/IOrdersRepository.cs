using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
namespace OnlineShop.Db.Repositories.Abstract
{
    public interface IOrdersRepository
    {
        Task SaveAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetAsync(Guid orderId);

		List<Order> GetAllUserOrdersAsync(string userId);

        Task EditStatusAsync(Guid orderId, OrderStatus status);
    }
} 
