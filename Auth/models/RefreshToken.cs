namespace Auth.Models;

public class RefreshToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public RefreshToken(string token, DateTime expiry)
    {
        Token = token;
        ExpiryDate = expiry;
    }
}