using BlazorApp1.Services.DataBase.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services.DataBase.Repository;

public class DatabaseRepository<TItem> : IDatabaseRepository<TItem> where TItem : DbItem
{
    private readonly ApplicationDbContext _context;

    public DatabaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TItem>> GetAll()
    {
        return await _context.Set<TItem>().ToListAsync();
    }

    public async Task<TItem?> GetById(Guid id)
    {
        return await _context.Set<TItem>().FindAsync(id);
    }

    public async Task Add(TItem item)
    {
        await _context.Set<TItem>().AddAsync(item);
    }

    public async Task Update(TItem item)
    {
        var dbSet = _context.Set<TItem>();
        var existingItem = await dbSet.FindAsync(item.Id);
        if (existingItem != null)
        {
            _context.Entry(existingItem).CurrentValues.SetValues(item);
        }
        else
        {
            throw new InvalidOperationException("Item não encontrado no banco de dados.");
        }
    }

    public async Task Delete(TItem item)
    {
        _context.Set<TItem>().Remove(item);
        await Task.CompletedTask;
    }

    public async Task<ICollection<TItem>?> GetWithQuery(Func<IQueryable<TItem>, IQueryable<TItem>> query)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        var dbSet = _context.Set<TItem>();
        var result = await query(dbSet).ToListAsync();
        return result;
    }
}