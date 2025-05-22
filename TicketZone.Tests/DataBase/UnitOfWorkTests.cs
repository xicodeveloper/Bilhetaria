using Xunit;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Tests.Services.DataBase.Repository
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public void GetRepository_ReturnsSameInstance()
        {
            // Act
            var repo1 = _unitOfWork.GetRepository<WalletUser>();
            var repo2 = _unitOfWork.GetRepository<WalletUser>();

            // Assert
            Assert.Same(repo1, repo2);
        }

        [Fact]
        public void Commit_CallsSaveChanges()
        {
            // Act
            var changes = _unitOfWork.Commit();

            // Assert
            Assert.Equal(0, changes); 
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}