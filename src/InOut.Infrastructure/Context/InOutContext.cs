using InOut.Domain.Entities;
using InOut.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InOut.Infrastructure.Context
{
    public class InOutContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public InOutContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BusinessBilling> BusinessBillings { get; set; }
        public DbSet<UserBusiness> UserBusinesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new BillingMapping());
            modelBuilder.ApplyConfiguration(new BusinessMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }
    }
}