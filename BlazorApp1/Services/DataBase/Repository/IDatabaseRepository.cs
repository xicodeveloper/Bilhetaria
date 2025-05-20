using BlazorApp1.Services.DataBase.DBEntities;

namespace BlazorApp1.Services.DataBase.Repository;

public interface IDatabaseRepository<TItem> where TItem : DbItem
{
    Task<List<TItem>> GetAll();
    Task<TItem?> GetById(Guid id);
    Task Add(TItem item);
    Task Update(TItem item);
    Task Delete(TItem item);
    Task<ICollection<TItem>?> GetWithQuery(Func<IQueryable<TItem>, IQueryable<TItem>> query);

}
