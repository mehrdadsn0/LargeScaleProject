namespace NotificationProject.Models;

public class EmailMessage
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public bool Succes { get; set; }
}