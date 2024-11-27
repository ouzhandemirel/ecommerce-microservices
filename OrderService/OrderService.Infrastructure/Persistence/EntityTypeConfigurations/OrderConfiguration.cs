using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.EntityTypeConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasIndex(x => x.CustomerId);

        builder.Ignore(x => x.IntegrationEvents);

        builder.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
    }
}