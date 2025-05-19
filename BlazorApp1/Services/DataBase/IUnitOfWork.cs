// Services/DataBase/IUnitOfWork.cs
using System;
using System.Threading.Tasks;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.Purchase;

namespace BlazorApp1.Services.DataBase
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IRepositoryWallet<WalletUser> WalletUsers { get; }
        
        Task<int> CommitAsync();
        int Commit();
        Task<int> CompleteAsync(); // Added to match interface
    }
}