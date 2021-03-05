using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarRentalContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-99FQ493\SQLEXPRESS;Database=CarRental;Trusted_Connection=true");
            //optionsBuilder.UseSqlServer(@"Server=ASSR\SQLEXPRESS;Database=CarRental;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }        
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<OperationClaim>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<UserOperationClaim>().HasIndex(u => new { u.UserId, u.OperationClaimId }).IsUnique(true);

            modelBuilder.Entity<Color>().HasIndex(x => x.ColorName).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(x => x.BrandName).IsUnique();
            modelBuilder.Entity<CarImage>().HasIndex(x => x.ImagePath).IsUnique();
        }
    }
}