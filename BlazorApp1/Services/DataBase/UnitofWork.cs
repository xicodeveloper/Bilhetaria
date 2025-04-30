using BlazorApp1.Services.DataBase;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IUserRepository User { get; }
    public IOrderRepository Order { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        User = new UserRepository(_context);
        Order = new OrderRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}