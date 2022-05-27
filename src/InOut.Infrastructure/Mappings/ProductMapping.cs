using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(220)")
                .HasMaxLength(220);

            builder.Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .HasColumnType("DECIMAL(10, 2)")
                .HasMaxLength(120);

            builder.Property(x => x.UnitOfMeasurement)
                .HasColumnName("UnitOfMeasurement")
                .HasColumnType("SMALLINT")
                .HasMaxLength(120);
        }
    }
}