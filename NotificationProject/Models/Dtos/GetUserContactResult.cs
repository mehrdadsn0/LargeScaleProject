using System.Text.Json.Serialization;

namespace NotificationProject.Models.Dtos;

public record GetUserContactResult
{
    [JsonPropertyName("number")]
    public string? Number { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
}