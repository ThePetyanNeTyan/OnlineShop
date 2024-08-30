using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class UserMapperConfiguration:Profile
	{
		public UserMapperConfiguration() 
		{
			CreateMap<User, UserViewModel>()
				.ForMember(p=>p.UploadedFiles,x=>x.Ignore());
			CreateMap<UserViewModel, User>()
				.ForMember(p => p.Id, x => x.Ignore())
				.ForMember(p => p.EmailConfirmed, x => x.Ignore())
				.ForMember(p => p.AccessFailedCount, x => x.Ignore())
				.ForMember(p => p.PhoneNumberConfirmed, x => x.Ignore())
				.ForMember(p => p.ConcurrencyStamp, x => x.Ignore())
				.ForMember(p => p.LockoutEnabled, x => x.Ignore())
				.ForMember(p => p.LockoutEnd, x => x.Ignore())
				.ForMember(p => p.NormalizedEmail, x => x.Ignore())
				.ForMember(p => p.NormalizedUserName, x => x.Ignore())
				.ForMember(p => p.PasswordHash, x => x.Ignore())
				.ForMember(p => p.TwoFactorEnabled, x => x.Ignore())
				.ForMember(p => p.SecurityStamp, x => x.Ignore());
				

				
		}
	}
}
