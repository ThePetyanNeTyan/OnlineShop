using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get;  set; }
 
        public string Name { get; set; }
 
        public decimal Cost { get; set; }
    
        public string Description { get; set; }

        public List<string>? ImgPath { get; set; } = new List<string>();

        public int? Bonus { get; set; }

        public List<CartProduct>? CartProducts { get; set; }

        public List<Favorite>? Favorites { get; set; }

        public List<Comparison>? Comparisons { get; set; }
    }
}
