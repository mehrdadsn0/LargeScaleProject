using Microsoft.EntityFrameworkCore;

namespace ProductProject.Models;

public class ProductsContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=largeScaleProject_Products;Username=mehrdad;Password=1234");
    }

    public DbSet<Product> Products { get; set; }
}