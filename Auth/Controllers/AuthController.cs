using Auth.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task SignUp(SignUpRequestDto input){
        
    }
}