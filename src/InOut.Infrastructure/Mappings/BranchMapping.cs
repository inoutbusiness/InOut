using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class BranchMapping : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                   .UseIdentityColumn()
                   .HasColumnType("BIGINT")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("VARCHAR(70)")
                   .HasMaxLength(220);

            builder.Property(x => x.Cnpj)
                   .IsRequired()
                   .HasColumnName("Cnpj")
                   .HasColumnType("VARCHAR(14)")
                   .HasMaxLength(14);

            builder.HasOne(a => a.Location)
                   .WithOne(b => b.Branch)
                   .HasForeignKey<Branch>(b => b.LocationId);

            builder.HasMany(a => a.Employees)
                   .WithOne(b => b.Branch)
                   .HasForeignKey(b => b.BranchId);
        }
    }
}