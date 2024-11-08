using System.ComponentModel.DataAnnotations;

namespace NotificationProject.Models.Dtos;

public record SendEmailDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
}
