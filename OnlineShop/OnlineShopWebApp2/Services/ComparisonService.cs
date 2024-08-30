using OnlineShop.Db.Models;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Services
{
    public class ComparisonService: IService<Comparison>
    {
        public Comparison Add(Product product, Comparison comparison)
        {

            if (comparison == null)
            {
                comparison = new Comparison();

                comparison.Products.Add(product);

                return comparison;

            }
            else
            {
                if (!comparison.Products.Any(p => p.Id == product.Id)) comparison.Products.Add(product);

                return comparison;
            }
        }

        public Comparison Remove(Guid productId, Comparison comparison)
        {
            var product = comparison.Products.FirstOrDefault(p => p.Id == productId);

            comparison.Products.Remove(product);

            return comparison;
        }
    }
}