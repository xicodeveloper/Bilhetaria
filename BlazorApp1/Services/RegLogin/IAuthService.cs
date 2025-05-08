
using System.Security.Claims;

namespace BlazorApp1.Services.RegLogin
{
    public interface IAuthService
    {
        
        Task<bool> UpdateUser( int id, string username, string email, string newPassword, bool sameUserName);
        Task<User> FindUserByUsername(string username);

        Task<bool> CheckUsernameExists(string username);
        Task<bool> CheckEmailExists(string email);

        Task CreateUser(string username, string password, string email);
        Task<AuthResult> LoginAsync(string username, string password);
        Task<bool> VerifyUserAsync(string email, string code);
        //Task<AuthResult> GetPersistedUserAsync();
        //Task PersistUserAsync(ClaimsPrincipal user);
        //Task ClearPersistedUserAsync();
        Task LogoutAsync();
        Task<int> GetUserId();
    }
}