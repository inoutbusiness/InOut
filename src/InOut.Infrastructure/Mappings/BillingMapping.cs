using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class BillingMapping : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.ToTable("Billings");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Value)
                .HasColumnName("Quantity")
                .HasColumnType("DECIMAL(10, 2)")
                .HasMaxLength(120);

            builder.Property(x => x.CustomerId)
                .HasColumnName("Quantity")
                .HasColumnType("DECIMAL(10, 2)")
                .HasMaxLength(120);
        }
    }
}