using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public Task<Order> GetOrder()
        {
            var order = dbContext.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .First();

            return Task.FromResult<Order>(order);
        }

        public Task<List<Order>> GetOrders()
        {
            var orders = dbContext.Orders
                .Where(o => o.User.Status == UserStatus.Active)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            return Task.FromResult<List<Order>>(orders);
        }
    }
}
