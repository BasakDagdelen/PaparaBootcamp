using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Models;

namespace Patikadev_RestfulApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
  
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(x => x.ProductId);
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Description).IsRequired(false);
            entity.Property(x => x.Price).IsRequired();
            entity.Property(x => x.Stock).IsRequired();
            entity.Property(x => x.IsAvailable).IsRequired();
        });

    }
}
