using InOut.Domain.Entities;
using InOut.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InOut.Infrastructure.Context
{
    public class InOutContext : DbContext
    {
        public InOutContext(DbContextOptions<InOutContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupom> Coupons { get; set; }

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
    }
}