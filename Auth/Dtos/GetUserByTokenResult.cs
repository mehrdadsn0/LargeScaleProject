namespace Auth.Dtos;

public record GetUserByTokenResult
{
    public int? Id { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}