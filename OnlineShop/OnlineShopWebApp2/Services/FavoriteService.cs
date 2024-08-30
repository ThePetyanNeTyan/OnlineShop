using OnlineShop.Db.Models;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Services
{
    public class FavoriteService: IService<Favorite>
    {
        public Favorite Add(Product product, Favorite favorite)
        {

            if (favorite == null)
            {
                favorite = new Favorite();

                favorite.Products.Add(product);

                return favorite;

            }
            else
            {
                if (!favorite.Products.Any(p => p.Id == product.Id)) favorite.Products.Add(product);

                return favorite;
            }
        }

        public Favorite Remove(Guid productId, Favorite favorite)
        {
            var product = favorite.Products.FirstOrDefault(p => p.Id == productId);

            favorite.Products.Remove(product);

            return favorite;
        }
    }
}
