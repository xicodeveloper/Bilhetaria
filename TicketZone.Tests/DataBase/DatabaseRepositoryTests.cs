using BlazorApp1.Services.DataBase;
using Xunit;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Tests.Services.DataBase.Repository
{
    public class DatabaseRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseRepository<WalletUser> _repository;

        // No DatabaseRepositoryTests.cs
        public DatabaseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
    
            // Criar usuário COM TODOS OS CAMPOS OBRIGATÓRIOS
            _context.Users.Add(new User 
            { 
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashed_password", // Campo obrigatório
                IsSucess = true 
            });
    
            _context.SaveChanges(); 
    
            _repository = new DatabaseRepository<WalletUser>(_context);
        }

        [Fact]
        public void Add_InsertsEntity()
        {
            // Arrange
            var item = new WalletUser { 
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid() 
            };

            // Act
            _repository.Add(item);
            _context.SaveChanges();

            // Assert
            var result = _context.Set<WalletUser>().Find(item.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_ReturnsEntity()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new WalletUser { 
                Id = id,
                UserId = Guid.NewGuid()
            };
            
            _repository.Add(expected);
            _context.SaveChanges();

            // Act
            var result = _repository.GetById(id);

            // Assert
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}