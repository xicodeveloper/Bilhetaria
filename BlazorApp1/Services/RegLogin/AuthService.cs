using System.Security.Claims;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Services.RegLogin
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VerificationService _verificationService;

        public AuthService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            VerificationService verificationService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _verificationService = verificationService;
        }

        public async Task<User> FindUserByUsername(string username)
        {
            return _unitOfWork.User.GetAll().FirstOrDefault(u => u.Username == username);
        }

        public async Task<bool> UpdateUser(int id, string username, string email, string newPassword, bool sameUserName)
        {
            var user = _unitOfWork.User.GetById(id);
            if (user == null) throw new KeyNotFoundException("Usuário não encontrado");

            if (user.Username != username && await CheckUsernameExists(username) && sameUserName)
                throw new InvalidOperationException("Nome de usuário já está em uso!");

            if (user.Email != email && await CheckEmailExists(email))
                throw new InvalidOperationException("Email já está em uso!");

            user.Username = username;
            user.Email = email;

            if (!string.IsNullOrEmpty(newPassword))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> VerifyUserAsync(string email, string code)
        {
            var user = _unitOfWork.User.GetAll().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            if (_verificationService.ValidateCode(email, code))
            {
                user.IsSucess = true;
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            return _unitOfWork.User.GetAll().Any(u => u.Username == username);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return _unitOfWork.User.GetAll().Any(u => u.Email == email);
        }

        public async Task CreateUser(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsSucess = false
            };

            _unitOfWork.User.Add(user);
            _unitOfWork.Complete();

            await _verificationService.SendVerificationCodeAsync(email);
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = await FindUser(username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new AuthResult { Success = false, ErrorMessage = "Credenciais inválidas" };
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                if (user.IsSucess)
                {
                    claims.Add(new Claim("EmailConfirmed", "true"));
                }

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                return new AuthResult
                {
                    Success = true,
                    ClaimsPrincipal = principal
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<User> FindUser(string usernameOrEmail)
        {
            return _unitOfWork.User.GetAll()
                .FirstOrDefault(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }

        public async Task LogoutAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
