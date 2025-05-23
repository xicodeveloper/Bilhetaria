using BlazorApp1.Services.DataBase.DBEntities;

namespace BlazorApp1.Services.RegLogin
{
    public interface IAuthService
    {
        
        bool UpdateUser(Guid id, string username, string email, string newPassword, bool sameUserName);
        User FindUserByUsername(string username);

        bool CheckUsernameExists(string username);
        bool CheckEmailExists(string email);

        Task CreateUser(string username, string password, string email);
        Task<AuthResult> LoginAsync(string username, string password);
        bool VerifyUser(string email, string code);
        Task LogoutAsync();
    }
}