using Domain;
using Domain.DTO_s.UserDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService context) : ControllerBase
{
    [HttpGet("get-Users")]
    public async  Task<PagedResponse<List<GetUsersDto>>> GetUserAsync([FromQuery]UserFilter filter)
    {
        return await context.GetUsersAsync(filter);
    }

    [HttpPost("create-User")]
    public async Task<Response<string>> AddUserAsync([FromForm]AddUserDto User)
    {
        return await context.AddUserAsync(User);
    }

    [HttpPut("update-User")]
    public async Task<Response<string>> UpdateUserAsync([FromForm]UpdateUserDto User)
    {
        return await context.UpdateUserAsync(User);
    }

    [HttpDelete("Delete User")]
    public async Task<Response<bool>> DeleteUserAsync(int id)
    {
        return await context.DeleteUserAsync(id);
    }

    [HttpGet("get-User-By-Id")]
    public async Task<Response<GetUsersDto>> GetUserById(int id)
    {
        return await context.GetUserByIdAsync(id);
    }
}