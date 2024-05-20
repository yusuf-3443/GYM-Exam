using Domain;
using Domain.DTO_s.MembershipDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MembershipServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MembershipController(IMembershipService context) : ControllerBase
{
    [HttpGet("get-Memberships")]
    public async  Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipAsync([FromQuery]MembershipFilter filter)
    {
        return await context.GetMembershipsAsync(filter);
    }

    [HttpPost("create-Membership")]
    public async Task<Response<string>> AddMembershipAsync([FromForm]AddMembershipDto Membership)
    {
        return await context.AddMembershipAsync(Membership);
    }

    [HttpPut("update-Membership")]
    public async Task<Response<string>> UpdateMembershipAsync([FromForm]UpdateMembershipDto Membership)
    {
        return await context.UpdateMembershipAsync(Membership);
    }

    [HttpDelete("Delete Membership")]
    public async Task<Response<bool>> DeleteMembershipAsync(int id)
    {
        return await context.DeleteMembershipAsync(id);
    }

    [HttpGet("get-Membership-By-Id")]
    public async Task<Response<GetMembershipsDto>> GetMembershipById(int id)
    {
        return await context.GetMembershipByIdAsync(id);
    }
}