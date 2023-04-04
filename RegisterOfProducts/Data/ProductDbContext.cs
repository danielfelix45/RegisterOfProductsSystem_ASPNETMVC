using Microsoft.EntityFrameworkCore;
using RegisterOfProducts.Models;

namespace RegisterOfProducts.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Description)
                .HasMaxLength(150);


            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<UserModel>()
                .Property(U => U.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<UserModel>()
                .Property(U => U.Email)
                .HasMaxLength(80);

            modelBuilder.Entity<UserModel>()
                .Property(U => U.Password)
                .HasMaxLength(80);

            modelBuilder.Entity<UserModel>()
                .Property(U => U.Login)
                .HasMaxLength(50);
        }
    }
}
