using Domain.DTO_s.ClassSheduleDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ClassSheduleServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassScheduleController(IClassScheduleService context) : ControllerBase
{
    [HttpGet("get-ClassSchedules")]
    public async  Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassScheduleAsync([FromQuery]ClassSheduleFilter filter)
    {
        return await context.GetClassSchedulesAsync(filter);
    }

    [HttpPost("create-ClassSchedule")]
    public async Task<Response<string>> AddClassScheduleAsync([FromForm]AddClassScheduleDto ClassSchedule)
    {
        return await context.AddClassScheduleAsync(ClassSchedule);
    }

    [HttpPut("update-ClassSchedule")]
    public async Task<Response<string>> UpdateClassScheduleAsync([FromForm]UpdateClassScheduleDto ClassSchedule)
    {
        return await context.UpdateClassScheduleAsync(ClassSchedule);
    }

    [HttpDelete("Delete ClassSchedule")]
    public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
    {
        return await context.DeleteClassSchedule(id);
    }

    [HttpGet("get-ClassSchedule-By-Id")]
    public async Task<Response<GetClassSchedulesDto>> GetClassScheduleById(int id)
    {
        return await context.GetClassScheduleByIdAsync(id);
    }
}