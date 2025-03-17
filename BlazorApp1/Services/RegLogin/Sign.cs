using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using BlazorApp1.Services;
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
        public async Task CreateUser(string username, string password, int id, string email)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = "INSERT INTO users (username, password, id, email) VALUES (@username, @password, @id, @email)";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@email", email);
            await command.ExecuteNonQueryAsync();
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
        public async Task<bool> ValidateLogin(string username, string password)
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            var result = (long)await command.ExecuteScalarAsync();
            return result > 0;
        }
    }
}