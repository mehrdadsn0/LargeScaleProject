namespace Auth.Dtos;

public record SignInRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}