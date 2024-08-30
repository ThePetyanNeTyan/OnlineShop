using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
    public class RoleMapperConfiguration:Profile
    {
        public RoleMapperConfiguration() 
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}
