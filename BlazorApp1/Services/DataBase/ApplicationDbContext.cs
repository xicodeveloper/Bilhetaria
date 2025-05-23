
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Services.DataBase.DBEntities;
using BlazorApp1.Services.DataBase.DBEntities.BasketItems;

namespace BlazorApp1.Services.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<TicketMovie> TicketMovies { get; set; }
        public DbSet<RentalMovie> RentalMovies { get; set; }
        public DbSet<PhysicalMovie> PhysicalMovies { get; set; }
        public DbSet<DigitalMovie> DigitalMovies { get; set; }
        public DbSet<WalletUser> WalletUser { get; set; }
        public DbSet<Address> Addresses { get; set; } 
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<BasketItem>().UseTpcMappingStrategy();
            
            modelBuilder.Entity<TicketMovie>().ToTable("movie_Tickets");
            modelBuilder.Entity<RentalMovie>().ToTable("rental_movies");
            modelBuilder.Entity<PhysicalMovie>().ToTable("physical_movies");
            modelBuilder.Entity<DigitalMovie>().ToTable("digital_movies");
            
            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.HasOne(bi => bi.Order)
                    .WithMany(b => b.Items)
                    .HasForeignKey(bi => bi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(bi => bi.Movie)
                    .WithMany()
                    .HasForeignKey(bi => bi.MovieId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // Configurar o nome das tabelas
            modelBuilder.Entity<User>().ToTable("users");
            
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany()
                .UsingEntity(j => j.ToTable("movie_has_genres"));

            modelBuilder.Entity<TicketMovie>().ToTable("ticket_movies");
            modelBuilder.Entity<RentalMovie>().ToTable("rental_movies");
            modelBuilder.Entity<PhysicalMovie>().ToTable("physical_movies");
            modelBuilder.Entity<DigitalMovie>().ToTable("digital_movies");
            
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<DBEntities.Address>().ToTable("addresses"); // Corrigido o nome
            
            // Configurar relacionamento User -> Address
            modelBuilder.Entity<DBEntities.Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            // Configurar relacionamento Order -> Address
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            
            
            modelBuilder.Entity<WalletUser>(entity =>
            {
                entity.ToTable("wallet_users");
                entity.HasKey(w => w.Id);

                entity.Property(w => w.MbwaySaldo)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(100m);

                entity.Property(w => w.ApplePaySaldo)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(100m);

                entity.Property(w => w.CreditCardSaldo)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(100m);

                entity.HasOne(w => w.User)
                    .WithOne(u => u.Wallet)
                    .HasForeignKey<WalletUser>(w => w.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


        }
        
    }
}