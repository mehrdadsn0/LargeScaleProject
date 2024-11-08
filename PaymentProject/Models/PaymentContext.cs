using Microsoft.EntityFrameworkCore;

namespace PaymentProject.Models;

public class PaymentContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Payments;Username=mehrdad;Password=1234");
    }
}