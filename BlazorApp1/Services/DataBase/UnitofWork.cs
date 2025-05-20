

using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.Repository;

namespace BlazorApp1.Services.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();
        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositoryFactory = new RepositoryFactory(_context);
        }


        public IDatabaseRepository<TEntity>? GetRepository<TEntity>() where TEntity : DbItem
        {
            if(!repositories.ContainsKey(typeof(TEntity)))
            {
                repositories[typeof(TEntity)] = _repositoryFactory.CreateRepository<TEntity>();
            }
            return (IDatabaseRepository<TEntity>)repositories[typeof(TEntity)];
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await CommitAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}