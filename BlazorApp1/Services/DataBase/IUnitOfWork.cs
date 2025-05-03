// Services/Core/UnitOfWork/IUnitOfWork.cs
using System;
using System.Threading.Tasks;


namespace BlazorApp1.Services.DataBase
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IRepositoryWallet<WalletUser> WalletUsers { get; } // Add this line
        Task<int> CommitAsync();
        int Commit();
    }
}