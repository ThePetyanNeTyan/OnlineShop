using AutoMapper;
using OnlineShopWebApp.Helpers.Profiles;

namespace OnlineShopWebApp.Helpers
{
    public  class MapperHelper
    {
        public static IMapper Mapper { get; set; }

        public static void CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(p =>
            {
                p.AddProfile<ProductMapperConfiguration>();
                p.AddProfile<CartMapperConfiguration>();
                p.AddProfile<CartProductMapperConfiguration>();
                p.AddProfile<OrderMapperConfiguration>();
                p.AddProfile<OderDetailsMapperConfiguration>();
                p.AddProfile<UserMapperConfiguration>();
                //p.AddProfile<RoleMapperConfiguration>();


            });
            mapperConfiguration.AssertConfigurationIsValid();
            Mapper=mapperConfiguration.CreateMapper();
        }

	}
}
