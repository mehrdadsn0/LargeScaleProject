using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth.Dtos;
using Auth.Dtos.Jwt;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Jwt;

namespace Auth.Controllers;

[ApiController]
[Route("auth")]
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

    

    [HttpGet("getusercontact/{id}")]
    public ActionResult<GetUserContactResult> GetUserContact([FromRoute] int id)
    {
        User? user = _authService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        else
        {
            GetUserContactResult response = new GetUserContactResult()
            {
                Number = user.PhoneNumber,
                Email = user.Email
            };
            return Ok(response);
        }
    }

    [HttpGet("getuserbytoken/{token}")]
    public ActionResult<GetUserByTokenResult> GetUserByToken(string token)
    {

        // Create an instance of JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Read the token (does not validate signature)
        var jwtToken = tokenHandler.ReadJwtToken(token);

        // Extract claims from the payload
        var claims = jwtToken.Claims;

        return new GetUserByTokenResult()
        {
            Id = int.Parse(claims.FirstOrDefault(c => c.Type == "Id")!.Value),
            Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value,
            Phone = claims.FirstOrDefault(c => c.Type == "Phone")!.Value
        };
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
            Password = signUpRequestDto.Password,
        }, true);

    }


    [HttpPost("signin")]
    public ActionResult SignIn([FromBody] SignInRequestDto signInRequestDto, bool fromSignUp = false)
    {
        var (res, message) = _authService.SignIn(signInRequestDto);
        var user = _authService.GetUserByEmail(signInRequestDto.Email);

        if (!res)
        {
            return BadRequest(message);
        }

        if (fromSignUp || res)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user!.Id.ToString()),
                new Claim("Phone", user!.PhoneNumber),
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