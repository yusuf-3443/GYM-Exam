using Domain.DTO_s.MembershipDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MembershipServices;

public interface IMembershipService
{
    Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipsAsync(MembershipFilter filter);
    Task<Response<GetMembershipsDto>> GetMembershipByIdAsync(int MembershipId);
    Task<Response<string>> AddMembershipAsync(AddMembershipDto AddMembership);
    Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto update);
    Task<Response<bool>> DeleteMembershipAsync(int MembershipId);

}