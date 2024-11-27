using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.EntityTypeConfigurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.SentAt);

        builder.Ignore(x => x.IntegrationEvents);

        builder.HasOne(x => x.Customer).WithMany(x => x.Notifications).HasForeignKey(x => x.CustomerId);
    }
}
