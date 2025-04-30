// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.OrderFiles;

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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().ToTable("movies");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Basket>().ToTable("baskets");
            modelBuilder.Entity<BasketItem>().ToTable("basket_items");
        }
    }
}