using Domain.DTO_s.WorkoutDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.WorkoutService;

public interface IWorkoutService
{
    Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkoutsAsync(WorkoutFilter filter);
    Task<Response<GetWorkoutsDto>> GetWorkoutByIdAsync(int WorkoutId);
    Task<Response<string>> AddWorkoutAsync(AddWorkoutDto AddWorkout);
    Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto update);
    Task<Response<bool>> DeleteWorkoutAsync(int WorkoutId);

}