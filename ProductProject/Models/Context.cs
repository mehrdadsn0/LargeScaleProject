using Microsoft.EntityFrameworkCore;

namespace ProductProject.Models;

public class ProductsContext : DbContext
{
    public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Products;Username=mehrdad;Password=1234");
    }

    public DbSet<Product> Products { get; set; }
}