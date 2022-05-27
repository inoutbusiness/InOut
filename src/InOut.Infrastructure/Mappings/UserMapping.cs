using InOut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InOut.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(120)")
                .HasMaxLength(120);

            builder.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR(13)")
                .HasMaxLength(13);

            builder.Property(x => x.CpfCnpj)
                .HasColumnName("CpfCnpj")
                .HasColumnType("VARCHAR(14)")
                .HasMaxLength(14);
        }
    }
}