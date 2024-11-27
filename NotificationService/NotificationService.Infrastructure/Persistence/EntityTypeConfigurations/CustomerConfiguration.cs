using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.EntityTypeConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);

        builder.Ignore(x => x.IntegrationEvents);

        builder.HasMany(x => x.Notifications).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
    }
}
