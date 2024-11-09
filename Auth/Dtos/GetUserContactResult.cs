namespace Auth.Dtos;

public record GetUserContactResult
{
    public string? Number { get; set; }
    public string? Email { get; set; }
}