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

        
        public User FindUserByUsername(string username)
        {
            var repository = _unitOfWork.GetRepository<User>();
            
            
            var list = repository.GetWithQuery(
                q => q.Where(
                    u => u.Username == username));
            
            return list.FirstOrDefault();
        }

        public bool UpdateUser(Guid id, string username, string email, string newPassword, bool sameUserName)
        {
            var user = _unitOfWork.GetRepository<User>().GetById(id);
            if (user == null) throw new KeyNotFoundException("Usuário não encontrado");

            if (user.Username != username && CheckUsernameExists(username) && sameUserName)
                throw new InvalidOperationException("Nome de usuário já está em uso!");

            if (user.Email != email && CheckEmailExists(email))
                throw new InvalidOperationException("Email já está em uso!");

            user.Username = username;
            user.Email = email;

            if (!string.IsNullOrEmpty(newPassword))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _unitOfWork.Commit();
            return true;
        }

        public bool VerifyUser(string email, string code)
        {
            var user = _unitOfWork.GetRepository<User>().GetWithQuery(q => q.Where(u => u.Email == email)).FirstOrDefault();
            if (user == null) return false;

            if (_verificationService.ValidateCode(email, code))
            {
                user.IsSucess = true;
                _unitOfWork.Commit();
                return true;
            }

            return false;
        }

        public bool CheckUsernameExists(string username)
        {
            var exists = _unitOfWork.GetRepository<User>()
                .GetWithQuery(q => q.Where(u => u.Username == username))
                .Any();
            return exists;
        }

        public bool CheckEmailExists(string email)
        {
            var exists = _unitOfWork.GetRepository<User>()
                .GetWithQuery(q => q.Where(u => u.Email == email))
                .Any();
            return exists;
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

            _unitOfWork.GetRepository<User>().Add(user);
            _unitOfWork.Commit();

            var wallet = new WalletUser
            {
                UserId = user.Id,
                MbwaySaldo = 100m,
                ApplePaySaldo = 100m,
                CreditCardSaldo = 100m
            };

            _unitOfWork.GetRepository<WalletUser>().Add(wallet);
            _unitOfWork.Commit();

            await _verificationService.SendVerificationCodeAsync(email);
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = FindUserByUsername(username);

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

        
        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userId, out var id) ? id : 0;
        }
    }
}