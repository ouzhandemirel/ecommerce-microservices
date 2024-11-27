using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.Contexts;

public class NotificationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("NotificationDb"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
    
    public DbSet<Product> Products { get; init; }
    public DbSet<Customer> Costumers { get; init; }
    public DbSet<Notification> Notifications { get; init; }
}
