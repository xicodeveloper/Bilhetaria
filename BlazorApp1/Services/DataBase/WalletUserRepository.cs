// Services/Core/Repositories/RepositoryWallet.cs
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorApp1.Services.DataBase
{
    public class WalletUserRepository : IRepositoryWallet<WalletUser>
    {
        private readonly ApplicationDbContext _context;

        public WalletUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WalletUser entity)
        {
            await _context.WalletUser.AddAsync(entity);
        }

        public async Task<WalletUser> GetByIdAsync(int id)
        {
            return await _context.WalletUser.FindAsync(id);
        }
        
        public async Task<WalletUser> GetByUserIdAsync(int userId)
        {
            return await _context.WalletUser.FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public void Update(WalletUser entity)
        {
            _context.WalletUser.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.WalletUser.Remove(entity);
            }
        }
    }
}