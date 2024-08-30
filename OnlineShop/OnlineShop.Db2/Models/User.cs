
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    public class User:IdentityUser
    {
        public int Age { get; set; }

        public List<string>? ImgPath { get; set; } = new();
    }
}
