using InOut.Domain.Entities;
using InOut.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InOut.Infrastructure.Context
{
    public class InOutContext : DbContext, IInOutContext
    {
        private readonly IConfiguration _configuration;

        public InOutContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<BusinessBilling> BusinessBillings { get; set; }
        public DbSet<UserBusiness> UserBusinesses { get; set; }
        public DbSet<ProductProvider> ProductProviders { get; set; }
        public DbSet<UserMovement> UserMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new BusinessMapping());
            modelBuilder.ApplyConfiguration(new LocationMapping());
            modelBuilder.ApplyConfiguration(new BranchMapping());
            modelBuilder.ApplyConfiguration(new MovementMapping());
            modelBuilder.ApplyConfiguration(new ProviderMapping());
            modelBuilder.ApplyConfiguration(new UserMovementMapping());
            modelBuilder.ApplyConfiguration(new ProductProviderMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:InOutDefaultConnection"]);
        }
    }
}