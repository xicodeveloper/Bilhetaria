using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.Movies;
using BlazorApp1.Services.Orders.Models;
using BlazorApp1.Services;

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
        public DbSet<Adress> Addresses { get; set; } // Corrigido o nome para Address

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.ToTable("basket_items");
                entity.HasKey(e => e.Id);
        
                entity.HasOne(bi => bi.Basket)
                    .WithMany(b => b.Items)
                    .HasForeignKey(bi => bi.BasketId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // Configurar o nome das tabelas
            modelBuilder.Entity<Film>().ToTable("movies");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Basket>().ToTable("baskets");
            modelBuilder.Entity<BasketItem>().ToTable("basket_items");
            modelBuilder.Entity<Adress>().ToTable("addresses"); // Corrigido o nome

            // Configurar relacionamento Basket -> BasketItem
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.Items)
                .WithOne(i => i.Basket) // Adicionar navegação inversa
                .HasForeignKey(i => i.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento Order -> Basket
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Basket)
                .WithOne()
                .HasForeignKey<Order>(o => o.BasketId) // Usar BasketId como FK
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento User -> Address
            modelBuilder.Entity<Adress>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            // Configurar relacionamento Order -> Address
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}