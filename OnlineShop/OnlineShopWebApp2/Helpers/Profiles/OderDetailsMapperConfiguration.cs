using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers.Profiles
{
	public class OderDetailsMapperConfiguration:Profile
	{
		public OderDetailsMapperConfiguration()
		{
			CreateMap<OrderDetails, OrderDetailsViewModel>();
			CreateMap<OrderDetailsViewModel, OrderDetails>()
				.ForMember(p => p.Order, x => x.Ignore())
				.ForMember(p => p.OrderId, x => x.Ignore())
                .ForMember(p => p.Id, x => x.Ignore());


		}
	}
}
