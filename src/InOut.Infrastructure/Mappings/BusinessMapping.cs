using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class BusinessMapping : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("Businesses");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(220)")
                .HasMaxLength(220);

            builder.Property(x => x.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("VARCHAR(14)")
                .HasMaxLength(14);
        }
    }
}