using System.Security.Claims;
using Auth.Dtos;
using Auth.Dtos.Jwt;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Jwt;

namespace Auth.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;
    private readonly RefreshTokenService _refreshTokenService;

    public AuthController(AuthService service, TokenService tokenService, RefreshTokenService refreshTokenService)
    {
        _authService = service;
        _tokenService = tokenService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("signup")]
    public ActionResult<TokenResponse> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
    {
        var (res, message) = _authService.SignUp(signUpRequestDto);
        if (!res)
        {
            return BadRequest(message);
        }

        return SignIn(new()
        {
            Email = signUpRequestDto.Email,
            Password = signUpRequestDto.Password
        }, true);

    }

    
    [HttpPost("signin")]
    public ActionResult SignIn([FromBody] SignInRequestDto signInRequestDto, bool fromSignUp = false)
    {
        var (res, message) = _authService.SignIn(signInRequestDto);
        
        if (!res)
        {
            return BadRequest(message);
        }

        if (fromSignUp || res)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, signInRequestDto.Email),
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _refreshTokenService.SaveRefreshToken(signInRequestDto.Email, refreshToken);

            return Ok(new TokenResponse(accessToken, refreshToken.Token));
        }
        else
        {
            return BadRequest(message);
        }
    }
}