using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockService.Domain.Entities;

namespace StockService.Infrastructure.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products").HasKey(a => a.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Barcode).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.Ignore(x => x.IntegrationEvents);
    }
}