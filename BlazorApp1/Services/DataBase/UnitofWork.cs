// Services/Core/UnitOfWork/UnitOfWork.cs
using BlazorApp1.Services.DataBase;



namespace BlazorApp1.Services.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _users;
        private IOrderRepository _orders;
        private IRepositoryWallet<WalletUser> _walletUsers;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => 
            _users ??= new UserRepository(_context);
    
        public IOrderRepository Orders => 
            _orders ??= new OrderRepository(_context);
    
        public IRepositoryWallet<WalletUser> WalletUsers => 
            _walletUsers ??= new WalletUserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}