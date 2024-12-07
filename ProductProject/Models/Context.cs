using Microsoft.EntityFrameworkCore;

namespace ProductProject.Models;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }
}