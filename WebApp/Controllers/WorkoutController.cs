using Domain;
using Domain.DTO_s.WorkoutDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.WorkoutService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkOutController(IWorkoutService context) : ControllerBase
{
    [HttpGet("get-WorkOuts")]
    public async  Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkOutAsync([FromQuery]WorkoutFilter filter)
    {
        return await context.GetWorkoutsAsync(filter);
    }

    [HttpPost("create-WorkOut")]
    public async Task<Response<string>> AddWorkOutAsync([FromForm]AddWorkoutDto WorkOut)
    {
        return await context.AddWorkoutAsync(WorkOut);
    }

    [HttpPut("update-WorkOut")]
    public async Task<Response<string>> UpdateWorkOutAsync([FromForm]UpdateWorkoutDto WorkOut)
    {
        return await context.UpdateWorkoutAsync(WorkOut);
    }

    [HttpDelete("Delete WorkOut")]
    public async Task<Response<bool>> DeleteWorkOutAsync(int id)
    {
        return await context.DeleteWorkoutAsync(id);
    }

    [HttpGet("get-WorkOut-By-Id")]
    public async Task<Response<GetWorkoutsDto>> GetWorkOutById(int id)
    {
        return await context.GetWorkoutByIdAsync(id);
    }
}