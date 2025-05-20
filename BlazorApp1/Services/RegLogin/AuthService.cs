// Services/RegLogin/AuthService.cs
using System.Security.Claims;
using BlazorApp1.Services.DataBase;
using BlazorApp1.Services.DataBase.DBEntities;
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
            return await _unitOfWork.Users.FindByUsernameAsync(username);
        }

        public async Task<bool> UpdateUser(int id, string username, string email, string newPassword, bool sameUserName)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("Usuário não encontrado");

            if (user.Username != username && await CheckUsernameExists(username) && sameUserName)
                throw new InvalidOperationException("Nome de usuário já está em uso!");

            if (user.Email != email && await CheckEmailExists(email))
                throw new InvalidOperationException("Email já está em uso!");

            user.Username = username;
            user.Email = email;

            if (!string.IsNullOrEmpty(newPassword))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> VerifyUserAsync(string email, string code)
        {
            var user = await _unitOfWork.Users.FindByEmailAsync(email);
            if (user == null) return false;

            if (_verificationService.ValidateCode(email, code))
            {
                user.IsSucess = true;
                await _unitOfWork.CommitAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            return await _unitOfWork.Users.UsernameExistsAsync(username);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            return await _unitOfWork.Users.EmailExistsAsync(email);
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

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            var wallet = new WalletUser
            {
                UserId = user.Id,
                MbwaySaldo = 100m,
                ApplePaySaldo = 100m,
                CreditCardSaldo = 100m
            };

            await _unitOfWork.WalletUsers.AddAsync(wallet);
            await _unitOfWork.CommitAsync();

            await _verificationService.SendVerificationCodeAsync(email);
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = await FindUser(username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new AuthResult
                    {
                        Success = false,
                        ErrorMessage = "Credenciais inválidas"
                    };
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                if (user.IsSucess)
                {
                    claims.Add(new Claim("EmailVerified", "true"));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Autenticar o usuário
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

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
            return await _unitOfWork.Users.FindByUsernameOrEmailAsync(usernameOrEmail);
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<int> GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            return int.TryParse(userId, out var id) ? id : 0;
        }
    }
}