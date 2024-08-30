using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    { 
        public string Id { get; set; }

        public List<CartProductViewModel> CartProducts { get; set; }

        public decimal TotalPrice { get => CartProducts?.Sum(p => p.Cost) ?? 0; }

        private int ProductsInCart;

        public int AmountProducts 
        { 
            get =>ProductsInCart= CartProducts?.Sum(p => p.Amount) ?? 0;
            set => ProductsInCart= value;
        }

        public int TotalBonus { get => CartProducts?.Sum(p => p.Bonus) ?? 0; }
      
        public CartViewModel()=>CartProducts = new();

        

      
    }
}
