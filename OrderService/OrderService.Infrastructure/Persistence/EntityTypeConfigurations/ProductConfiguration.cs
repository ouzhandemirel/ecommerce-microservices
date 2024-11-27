using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.Ignore(x => x.IntegrationEvents);

        builder.HasMany(x => x.OrderItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
    }
}