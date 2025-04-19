// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.Movies;

namespace BlazorApp1.Services.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Film> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().ToTable("movies");
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}