using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.Ignore(x => x.IntegrationEvents);
    }
}
