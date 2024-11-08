namespace OrdersProject.Models.Dtos;

public record NotificationEventDto{
    public string? Number { get; set; }
    public string? Email { get; set; }
    public string Content { get; set; } = null!;
    public int? UserId { get; set; }
}