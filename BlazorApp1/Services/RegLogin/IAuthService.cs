
using System.Security.Claims;

namespace BlazorApp1.Services.RegLogin
{
    public interface IAuthService
    {
        Task<bool> CheckUsernameExists(string username);
        Task CreateUser(string username, string password, string email);
        Task<AuthResult> LoginAsync(string username, string password);
        Task<AuthResult> GetPersistedUserAsync();
        Task PersistUserAsync(ClaimsPrincipal user);
        Task ClearPersistedUserAsync();
        Task LogoutAsync();
    }
}