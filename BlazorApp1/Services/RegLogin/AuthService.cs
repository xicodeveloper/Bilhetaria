using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BlazorApp1.Services.DataBase;
using MySql.Data.MySqlClient;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Services.RegLogin
{
    

public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VerificationService _verificationService;

        public AuthService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            VerificationService verificationService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _verificationService = verificationService;
        }
        public async Task<bool> VerifyUserAsync(string email, string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            if (_verificationService.ValidateCode(email, code))
            {
                user.IsSucess = true; 
                Console.WriteLine("User success: " + user.IsSucess);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> CheckUsernameExists(string username)
            {
                return await _context.Users
                    .AnyAsync(u => u.Username == username);
            }    
        public async Task<bool> CheckEmailExists(string email)
            {
                return await _context.Users
                    .AnyAsync(u => u.Email == email);
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
    
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
    
                await _verificationService.SendVerificationCodeAsync(email); // Enviar código após criar usuário
            }

// Crie um endpoint temporário para teste

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

                // Adicione aqui a verificação do status de confirmação
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
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
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

