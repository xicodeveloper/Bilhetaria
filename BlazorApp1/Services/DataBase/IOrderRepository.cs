using BlazorApp1.Services.OrderFiles;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task UpdateAsync(Order order);

}