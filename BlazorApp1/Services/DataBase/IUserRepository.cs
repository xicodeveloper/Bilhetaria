// Services/Users/Interfaces/IUserRepository.cs

using System.Threading.Tasks;

namespace BlazorApp1.Services.DataBase
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUsernameOrEmailAsync(string usernameOrEmail);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}