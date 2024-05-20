using Domain.DTO_s.TrainerDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TrainerServices;

public interface ITrainerService
{
    Task<PagedResponse<List<GetTrainersDto>>> GetTrainersAsync(TrainerFilter filter);
    Task<Response<GetTrainersDto>> GetTrainerByIdAsync(int TrainerId);
    Task<Response<string>> AddTrainerAsync(AddTrainerDto AddTrainer);
    Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto update);
    Task<Response<bool>> DeleteTrainerAsync(int TrainerId);
}