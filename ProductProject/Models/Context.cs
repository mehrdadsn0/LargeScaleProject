using Microsoft.EntityFrameworkCore;

namespace ProductProject.Models;

public class ProductsContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=database:5432;Database=products;Username=mehrdad;Password=1234");
    }

    public DbSet<Product> Products { get; set; }
}