using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Services.Abstract
{
    public interface IService<T> where T : class
    {
        T Add(Product product, T obj);

        T Remove(Guid productId, T obj);
    }
}
