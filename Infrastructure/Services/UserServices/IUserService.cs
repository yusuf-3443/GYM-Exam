using Domain.DTO_s.UserDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.UserServices;

public interface IUserService
{
    Task<PagedResponse<List<GetUsersDto>>> GetUsersAsync(UserFilter filter);
    Task<Response<GetUsersDto>> GetUserByIdAsync(int userId);
    Task<Response<string>> AddUserAsync(AddUserDto AddUser);
    Task<Response<string>> UpdateUserAsync(UpdateUserDto update);
    Task<Response<bool>> DeleteUserAsync(int userId);
}