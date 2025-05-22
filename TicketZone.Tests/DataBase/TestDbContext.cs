using BlazorApp1.Services.DataBase;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlazorApp1.Tests.Services.DataBase
{
    public class TestDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }

    public class DatabaseTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public DatabaseTestBase()
        {
            _context = TestDbContextFactory.Create();
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }
    }
}