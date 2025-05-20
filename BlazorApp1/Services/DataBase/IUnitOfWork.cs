
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.Repository;

namespace BlazorApp1.Services.DataBase
{
    public interface IUnitOfWork : IDisposable
    {

        IDatabaseRepository<TEntity>? GetRepository<TEntity>() where TEntity : DbItem;
        Task<int> CommitAsync();
        int Commit();
        Task<int> CompleteAsync(); // Added to match interface
    }
}