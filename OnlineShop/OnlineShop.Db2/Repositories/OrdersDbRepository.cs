using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Abstract;

namespace OnlineShop.Db.Repositories
{
	public class OrdersDbRepository:IOrdersRepository
	{
		private readonly DatabaseContext databaseContext;

		public OrdersDbRepository(DatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public async Task SaveAsync(Order order)
		{
			order.OrderDetails.Id = order.Id;

			await databaseContext.Orders.AddAsync(order);

			await databaseContext.SaveChangesAsync();
		}

		public List<Order> GetAllUserOrdersAsync(string id)=>  databaseContext.Orders.Where(p=>p.UserId == id).ToList();
	
        public async Task<IEnumerable<Order>> GetAllAsync() => await databaseContext.Orders.Include(p=>p.OrderDetails).ToListAsync();

		public async Task<Order> GetAsync(Guid id) => await databaseContext.Orders.Include(p => p.OrderDetails).Include(p=>p.CartProducts).FirstOrDefaultAsync(p=>p.Id==id);

		public async Task EditStatusAsync(Guid id, OrderStatus status)
		{
			var order = await GetAsync(id);

			order.Status = status;

			await databaseContext.SaveChangesAsync();

		}
	}
}
