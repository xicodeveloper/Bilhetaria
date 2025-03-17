using System.Security.Claims;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using BlazorApp1.Services;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace BlazorApp1.Services.RegLogin
{
    public class Sign
    {
        private readonly string _connectionString =
            "Server=localhost;Port=3306;Database=ticketzone;Uid=root;Pwd=;";

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

        // Cria um novo utilizador
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
                command.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                command.Parameters.AddWithValue("@email", email);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Erro no banco de dados: " + ex.Message, ex);
            }
        }

        public async Task<User> FindUser(string usernameOrEmail)
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
            command.Parameters.AddWithValue("@identifier", usernameOrEmail);

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

        // Verifica as credenciais de login
        public async Task<AuthResult> ValidateLogin(string username, string password)
        {
            var user = await FindUser(username);
    
            if (user == null)
            {
                Console.WriteLine("Utilizador não encontrado");
                return new AuthResult { Success = false };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    
            if (!isPasswordValid)
            {
                Console.WriteLine("Password incorreta");
                return new AuthResult { Success = false };
            }

            // Cria claims de autenticação
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
    
            return new AuthResult 
            { 
                Success = true,
                ClaimsPrincipal = principal
            };
        }
    }
}
