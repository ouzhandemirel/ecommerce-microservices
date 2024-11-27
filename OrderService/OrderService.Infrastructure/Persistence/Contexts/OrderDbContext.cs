using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Contexts;

public class OrderDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("OrderDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }

    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderItem> OrderItems { get; init; }
    public DbSet<Product> Products { get; init; }

    
}
