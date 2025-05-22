using BlazorApp1.Services.DataBase;
using Xunit;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Tests.Services.DataBase.Repository
{
    public class DatabaseRepositoryIntegrationTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly ApplicationDbContext _context;
        private readonly Guid _userId;

        public DatabaseRepositoryIntegrationTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            // Criar usuário válido com todos os campos obrigatórios
            var user = new User(
                username: "testuser",
                email: "test@example.com",
                passwordHash: BCrypt.Net.BCrypt.HashPassword("senhaSegura123") // Campo obrigatório
            ){
                Id = Guid.NewGuid()
            };
            
            _userId = user.Id; // Guardar o ID para uso nos testes
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        [Fact]
        public void Add_ShouldPersistEntity()
        {
            // Arrange
            var repository = new DatabaseRepository<WalletUser>(_context);
            var wallet = new WalletUser 
            {
                Id = Guid.NewGuid(),
                UserId = _userId // Usar o ID do usuário criado
            };

            // Act
            repository.Add(wallet);
            _context.SaveChanges();

            // Assert
            var result = _context.Set<WalletUser>().Find(wallet.Id);
            Assert.NotNull(result);
            Assert.Equal(_userId, result.UserId);
        }

        public void Dispose()
        {
            _connection.Close();
            _context.Dispose();
        }
    }
}