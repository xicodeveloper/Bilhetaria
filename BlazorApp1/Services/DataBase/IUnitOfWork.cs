// Services/Core/UnitOfWork/IUnitOfWork.cs
using System;
using System.Threading.Tasks;


namespace BlazorApp1.Services.DataBase
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}