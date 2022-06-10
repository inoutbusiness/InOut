﻿using InOut.Domain.Entities;
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

            builder.Property(x => x.Brand)
                .HasColumnName("Brand")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("DECIMAL(10, 2)")
                .HasMaxLength(120);

            builder.Property(x => x.Color)
                .HasColumnName("Color")
                .HasColumnType("SMALLINT")
                .HasMaxLength(120);

            builder.Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .HasColumnType("DECIMAL(10, 2)")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("VARCHAR(500)")
                .HasMaxLength(500);

            builder.Property(x => x.UnitOfMeasurement)
                .HasColumnName("UnitOfMeasurement")
                .HasColumnType("SMALLINT")
                .HasMaxLength(120);

            builder.Property(x => x.Type)
                .HasColumnName("Type")
                .HasColumnType("SMALLINT")
                .HasMaxLength(120);
        }
    }
}