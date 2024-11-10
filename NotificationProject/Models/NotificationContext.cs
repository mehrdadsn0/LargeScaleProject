using Microsoft.EntityFrameworkCore;

namespace NotificationProject.Models;

public class NotificationContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Host=database:5432;Database=notification;Username=mehrdad;Password=1234");
    }

    public DbSet<SMSMessage> SMSMessages { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
}