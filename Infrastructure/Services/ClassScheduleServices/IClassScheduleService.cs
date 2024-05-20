using Domain.DTO_s.ClassSheduleDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ClassSheduleServices;

public interface IClassScheduleService
{
    Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassSchedulesAsync(ClassSheduleFilter filter);
    Task<Response<GetClassSchedulesDto>> GetClassScheduleByIdAsync(int ClassShedulesId);
    Task<Response<string>> AddClassScheduleAsync(AddClassScheduleDto addClassSchedules);
    Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto update);
    Task<Response<bool>> DeleteClassSchedule(int ClassShedulesId);

}