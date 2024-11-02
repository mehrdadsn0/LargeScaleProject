namespace Auth.Dtos.Jwt;

public record TokenResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken  { get; set; } = null!;
    public TokenResponse(string accessToken, string refreshToken)
    {
        this.AccessToken = accessToken;
        this.RefreshToken = refreshToken;
    }
}