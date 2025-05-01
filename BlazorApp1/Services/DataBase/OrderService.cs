// Exemplo de uso no serviço


using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.Orders.Models;

public class OrderService
{
    private readonly IUnitOfWork _uow;

    public OrderService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task CreateOrder(Order order)
    {
        await _uow.Orders.AddAsync(order);
        await _uow.CommitAsync();
    }

    public async Task<Order> GetUserOrder(int userId, int orderId)
    {
        return await _uow.Orders.GetByIdAsync(orderId);
    }
}