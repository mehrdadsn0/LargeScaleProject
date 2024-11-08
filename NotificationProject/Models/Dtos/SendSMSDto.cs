using System.ComponentModel.DataAnnotations;

namespace NotificationProject.Models.Dtos;

public record SendSMSDto
{
    [Required]
    public string Number { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
}
