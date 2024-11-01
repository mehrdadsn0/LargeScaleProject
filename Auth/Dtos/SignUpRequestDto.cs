namespace Auth.Dtos;

public record SignUpRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}