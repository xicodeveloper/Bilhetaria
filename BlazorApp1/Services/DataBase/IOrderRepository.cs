// Services/Orders/Interfaces/IOrderRepository.cs
using BlazorApp1.Services.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

 namespace BlazorApp1.Services.DataBase
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetActiveOrderWithItemsAsync(int userId);
        Task ClearBasketAsync(int orderId);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task UpdateAsync(Order order);
        Task SaveChangesAsync();
    }
}