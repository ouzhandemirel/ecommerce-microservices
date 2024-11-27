using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockService.Domain.Entities;

namespace StockService.Infrastructure.Persistence.Contexts;

public class StockDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public StockDbContext(DbContextOptions<StockDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("StockDb"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
    }
    
    public DbSet<Product> Products { get; init; }
}