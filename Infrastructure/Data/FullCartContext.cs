using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.UserAggregate;
using Domain.CustomerAggregate;
using Domain.ProductCollectionAggregate;
using Domain.OrderAggregate;

namespace Infrastructure.Data
{
    public class FullCartContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>//DbContext
    {
        #region Entities DbSets
        public DbSet<ProductStore> ProductStores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SpeceficationType> SpeceficationTypes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<ProductSpec> ProductSpec { get; set; }
        #endregion

        public FullCartContext(DbContextOptions<FullCartContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(e => e.Customer)
                .HasForeignKey(o => o.CustomerId);

            base.OnModelCreating(modelBuilder);


        }
    }
}
