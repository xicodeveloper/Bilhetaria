// Services/Orders/Repositories/OrderRepository.cs
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp1.Services.OrderFiles;

namespace BlazorApp1.Services.DataBase
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Basket)
                .ThenInclude(b => b.Items)
                .Include(o => o.ShippingAddress)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Basket)
                .ThenInclude(b => b.Items)
                .Include(o => o.ShippingAddress)
                .ToListAsync();
        }
        public async Task<Order> GetActiveOrderWithItemsAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Basket)
                .ThenInclude(b => b.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Pending);
        }
        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Basket)
                .ThenInclude(b => b.Items)
                .Include(o => o.ShippingAddress)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
  

        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task ClearBasketAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Basket)
                .ThenInclude(b => b.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order?.Basket?.Items != null)
            {
                _context.BasketItems.RemoveRange(order.Basket.Items);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task RemoveBasketItemAsync(int itemId)
        {
            var item = await _context.BasketItems.FindAsync(itemId);
            if (item != null)
            {
                _context.BasketItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task ReloadOrderAsync(Order order)
        {
            await _context.Entry(order).ReloadAsync();
            await _context.Entry(order.Basket).Collection(b => b.Items).LoadAsync();
        }
        public async Task<double> GetTotalPurchaseValueAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Completed)
                .SelectMany(o => o.Basket.Items)
                .SumAsync(i => i.Price * i.Quantity);
        }

        public async Task<IEnumerable<BasketItem>> GetUniqueMoviesAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Completed)
                .SelectMany(o => o.Basket.Items)
                .GroupBy(i => i.MovieId)
                .Select(g => g.First())
                .ToListAsync();
        }


    }
}