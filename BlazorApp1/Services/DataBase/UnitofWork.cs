// Services/DataBase/UnitOfWork.cs
using System;
using System.Threading.Tasks;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.Purchase;
using BlazorApp1.Services.Purchase.OrderState;
using BlazorApp1.Services.RegLogin;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services.Orders;

namespace BlazorApp1.Services.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _users;
        private IOrderRepository _orders;
        private IRepositoryWallet<WalletUser> _walletUsers;
        private IMovieRepository _movies;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IMovieRepository Movies => 
            _movies ??= new MovieRepository(_context);

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