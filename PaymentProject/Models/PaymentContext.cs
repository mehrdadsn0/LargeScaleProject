using Microsoft.EntityFrameworkCore;

namespace PaymentProject.Models;

public class PaymentContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=database:5432;Database=payments;Username=mehrdad;Password=1234");
    }

    public DbSet<Payment> Payments { get; set; }
}