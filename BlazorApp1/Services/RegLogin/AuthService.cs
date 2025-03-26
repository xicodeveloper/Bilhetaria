using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MySql.Data.MySqlClient;
using BlazorApp1.Services.RegLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Services.RegLogin
{
    

public class AuthService : IAuthService
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public  AuthService(IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            _connectionString = "Server=localhost;Port=3306;Database=ticketzone;Uid=root;Pwd=;";
            _httpContextAccessor= _httpContextAccessor;
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = "SELECT COUNT(*) FROM users WHERE username = @username";
            
            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            var result = (long)await command.ExecuteScalarAsync();
            return result > 0;
        }

        public async Task CreateUser(string username, string password, string email)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                const string query = @"INSERT INTO users
                             (username, password, email)
                             VALUES (@username, @password, @email)";

                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password",  BCrypt.Net.BCrypt.HashPassword(password));
                command.Parameters.AddWithValue("@email", email);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Erro no banco de dados: " + ex.Message, ex);
            }
        }

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
        private async Task<User?> FindUser(string identifier)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"SELECT
                            id,
                            username,
                            email,
                            password
                          FROM users
                          WHERE username = @identifier
                             OR email = @identifier
                          LIMIT 1";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@identifier", identifier);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    reader.GetString(reader.GetOrdinal("username")),
                    reader.GetString(reader.GetOrdinal("email")),
                    reader.GetString(reader.GetOrdinal("password"))
                );
            }

            return null;
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

