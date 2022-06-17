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

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("VARCHAR(60)")
                .HasMaxLength(60);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("VARCHAR(60)")
                .HasMaxLength(60);

            builder.Property(x => x.CpfCnpj)
                .IsRequired()
                .HasColumnName("CpfCnpj")
                .HasColumnType("VARCHAR(14)")
                .HasMaxLength(14);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR(13)")
                .HasMaxLength(13);

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnName("BirthDate")
                .HasColumnType("datetime2");

            builder.HasOne<Account>(a => a.Account)
                   .WithOne(b => b.User)
                   .HasForeignKey<Account>(b => b.UserId);

            builder.HasOne(a => a.Branch)
                   .WithMany(b => b.Employees)
                   .IsRequired();
        }
    }
}