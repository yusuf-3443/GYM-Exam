using Domain.DTO_s.Login_Register;
using Domain.Responses;
using Infrastructure.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService)
{
    [HttpPost("Login")]
    public async Task<Response<string>> Login([FromBody] LoginDto loginDto)
    => await authService.Login(loginDto);
    

}
