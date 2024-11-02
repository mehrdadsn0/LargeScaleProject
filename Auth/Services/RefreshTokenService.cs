using Auth.Models;

namespace Store.Services.Jwt;

public class RefreshTokenService
{
    private readonly Dictionary<string, RefreshToken> _refreshTokens = new Dictionary<string, RefreshToken>();
    public void DeleteRefreshToken(string username)
    {
        _refreshTokens.Remove(username);
    }

    public RefreshToken? GetRefreshToken(string username)
    {
        _refreshTokens.TryGetValue(username, out var refreshToken);
        return refreshToken;
    }

    public void SaveRefreshToken(string username, RefreshToken refreshToken)
    {
        _refreshTokens[username] = refreshToken;
    }
}