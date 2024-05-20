using System.Net;
using AutoMapper;
using Domain.DTO_s.MembershipDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MembershipServices;

public class MembershipService:IMembershipService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MembershipService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipsAsync(MembershipFilter filter)
    {
        try
        {
            var Memberships = _context.Memberships.AsQueryable();
            if (!string.IsNullOrEmpty(filter.StartDate.ToString()))
                Memberships = Memberships.Where(e => e.StartDate.ToString().ToLower().Contains(filter.StartDate.ToString().ToLower()));
            if (!string.IsNullOrEmpty(filter.EndDate.ToString()))
                Memberships = Memberships.Where(e => e.EndDate.ToString().ToLower().Contains(filter.EndDate.ToString().ToLower()));
            
            var result = await Memberships.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Memberships.CountAsync();
            var response = _mapper.Map<List<GetMembershipsDto>>(result);
            return new PagedResponse<List<GetMembershipsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMembershipsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMembershipsDto>> GetMembershipByIdAsync(int MembershipId)
    {
        try
        {

            var exist = await _context.Memberships.FindAsync(MembershipId);
            if (exist == null) return new Response<GetMembershipsDto>(HttpStatusCode.NotFound, "Membership Not Found!");

            var mapped = _mapper.Map<GetMembershipsDto>(exist);
            return new Response<GetMembershipsDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetMembershipsDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddMembershipAsync(AddMembershipDto AddMembership)
    {
        try
        {
            if (AddMembership == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<Membership>(AddMembership);
            await _context.Memberships.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto update)
    {
        try
        {
            var existing = await _context.Memberships.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<Membership>(update);
            _context.Memberships.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteMembershipAsync(int MembershipId)
    {
        try
        {
            var existing = await _context.Memberships.Where(e => e.Id == MembershipId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Membership not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}