using BlazorApp1.Services.DataBase.DBEntities;

namespace BlazorApp1.Services.DataBase.Repository;

public class RepositoryFactory(ApplicationDbContext context) : IRepositoryFactory
{
    private readonly ApplicationDbContext _context = context;
    
    public IDatabaseRepository<TItem> CreateRepository<TItem>() where TItem : DbItem
    {
        return new DatabaseRepository<TItem>(_context);
    }
}