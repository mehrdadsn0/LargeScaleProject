using Microsoft.EntityFrameworkCore;

namespace OrdersProject.Models;

public class OrdersContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Orders;Username=mehrdad;Password=1234");
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
}