using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InOut.Infrastructure.Context
{
    public interface IInOutContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Branch> Branches { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Movement> Movements { get; set; }
        DbSet<Business> Businesses { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Provider> Providers { get; set; }
        DbSet<BusinessBilling> BusinessBillings { get; set; }
        DbSet<UserBusiness> UserBusinesses { get; set; }
        DbSet<ProductProvider> ProductProviders { get; set; }
        DbSet<UserMovement> UserMovements { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Remove(object entity);
        EntityEntry Entry(object entity);
    }
}
