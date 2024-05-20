using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<User> GetUser()
        {
            var ordersWithUsers = dbContext.Orders
                .Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered)
                .Include(o => o.User)
                .Select(o => new { o.User, o.Quantity }).ToList();

            var user = ordersWithUsers.MaxBy(e => e.Quantity).User;

            return Task.FromResult<User>(user);
        }

        public Task<List<User>> GetUsers()
        {
            var users = dbContext.Orders
                .Where(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid)
                .Include(o => o.User)
                .Select(o => o.User).ToList();

            return Task.FromResult<List<User>>(users);
        }
    }
}
