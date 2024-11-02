using Microsoft.EntityFrameworkCore;

namespace Auth.Models;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Auth;Username=mehrdad;Password=1234");
    }

    public DbSet<User> Users { get; set; }
}