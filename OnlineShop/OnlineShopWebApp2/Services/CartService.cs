using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Services.Abstract;

namespace OnlineShopWebApp.Services
{
    public class CartService :IService<Cart>
    {

        public Cart Add(Product product, Cart cart)
        {

            var cartProduct = new CartProduct { Product = product, Amount = 1 };

            if (cart == null)
            {
                cart = new Cart() { };

                cart.CartProducts.Add(cartProduct);

                return cart;
            }

            else
            {
                if (!cart.CartProducts.Any(p => p.Product.Id == product.Id)) cart.CartProducts.Add(cartProduct);

                else cart.CartProducts.FirstOrDefault(p => p.Product.Id == product.Id).Amount += 1;

                return cart;
            }
        }

        public Cart Remove(Guid productId, Cart cart)
        {
            var product = cart.CartProducts.FirstOrDefault(p => p.Product.Id == productId);

            product.Amount -= 1;

            if (product.Amount == 0) cart.CartProducts.Remove(product);

            if (cart.CartProducts.Count == 0) cart.CartProducts.Clear();

            return cart;
        }
    }
}
