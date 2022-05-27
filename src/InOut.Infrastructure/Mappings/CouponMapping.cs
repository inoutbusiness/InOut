using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class CouponMapping : IEntityTypeConfiguration<Coupom>
    {
        public void Configure(EntityTypeBuilder<Coupom> builder)
        {
            builder.ToTable("Coupons");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(220)")
                .HasMaxLength(220);

            builder.Property(x => x.DiscountPercentage)
                .HasColumnName("DiscountPercentage")
                .HasColumnType("TINYINT")
                .HasMaxLength(100)
                .HasMaxLength(220);
        }
    }
}