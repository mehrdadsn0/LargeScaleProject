namespace NotificationProject.Models;

public class SMSMessage
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public bool Succes { get; set; }
}