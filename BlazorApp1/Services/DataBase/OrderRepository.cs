using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.OrderFiles;
using Microsoft.EntityFrameworkCore;

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

    // Busca pelo ID do pedido (não confundir com UserId)
    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Basket)
            .ThenInclude(b => b.Items)
            .Include(o => o.ShippingAddress)
            .FirstOrDefaultAsync(o => o.Id == id); // Corrigido para usar Id do pedido
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Basket)
            .ThenInclude(b => b.Items)
            .Include(o => o.ShippingAddress)
            .ToListAsync();
    }

    // Busca todos os pedidos de um usuário
    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.Basket)
            .ThenInclude(b => b.Items)
            .Include(o => o.ShippingAddress)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    // Busca o carrinho ativo (pedido não finalizado)

    // Atualiza um pedido
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}