// Services/Core/Repositories/IRepositoryWallet.cs
using System.Threading.Tasks;

namespace BlazorApp1.Services.DataBase
{
    public interface IRepositoryWallet<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByUserIdAsync(int userId);
        void Update(T entity);
        Task DeleteAsync(int id);
    }
}