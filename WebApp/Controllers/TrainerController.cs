using Domain.DTO_s.TrainerDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TrainerServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainerController(ITrainerService context) : ControllerBase
{
    [HttpGet("get-Trainers")]
    public async  Task<PagedResponse<List<GetTrainersDto>>> GetTrainerAsync([FromQuery]TrainerFilter filter)
    {
        return await context.GetTrainersAsync(filter);
    }

    [HttpPost("create-Trainer")]
    public async Task<Response<string>> AddTrainerAsync([FromForm]AddTrainerDto Trainer)
    {
        return await context.AddTrainerAsync(Trainer);
    }

    [HttpPut("update-Trainer")]
    public async Task<Response<string>> UpdateTrainerAsync([FromForm]UpdateTrainerDto Trainer)
    {
        return await context.UpdateTrainerAsync(Trainer);
    }

    [HttpDelete("Delete Trainer")]
    public async Task<Response<bool>> DeleteTrainerAsync(int id)
    {
        return await context.DeleteTrainerAsync(id);
    }

    [HttpGet("get-Trainer-By-Id")]
    public async Task<Response<GetTrainersDto>> GetTrainerById(int id)
    {
        return await context.GetTrainerByIdAsync(id);
    }
}