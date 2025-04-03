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
                Console.WriteLine(user.IsSucess);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
            public async Task<bool> CheckUsernameExists(string username)
            {
                return await _context.Users
                    .AnyAsync(u => u.Username == username);
            }    public async Task<bool> CheckEmailExists(string email)
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
                Console.WriteLine($"Iniciando login para: {username}");
        
                var user = await FindUser(username);
                Console.WriteLine($"Usuário encontrado: {user != null}");

                if (user == null)
                {
                    Console.WriteLine("Usuário não encontrado no banco de dados");
                    return new AuthResult { Success = false };
                }

                Console.WriteLine($"Dados do usuário encontrado:");
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                Console.WriteLine($"Senha válida: {isPasswordValid}");

                if (!isPasswordValid)
                {
                    return new AuthResult { Success = false };
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                Console.WriteLine("\nClaims geradas:");
                foreach (var claim in claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
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
                Console.WriteLine($"Erro durante o login: {ex}");
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
        public async Task<AuthResult> GetPersistedUserAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            if (result.Succeeded && result.Principal != null)
            {
                return new AuthResult 
                { 
                    Success = true,
                    ClaimsPrincipal = result.Principal
                };
            }
            
            return new AuthResult { Success = false };
        }
        
        public async Task PersistUserAsync(ClaimsPrincipal principal)
        {
            var context = _httpContextAccessor.HttpContext;
    
            if (context == null)
            {
                Console.WriteLine("HttpContext nulo durante persistência!");
                return;
            }

            // Adicione este log para verificar as claims antes de persistir
            Console.WriteLine("\nClaims antes de persistir no cookie:");
            foreach (var claim in principal.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                });
        }
        public async Task ClearPersistedUserAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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

