using System.Net;
using AutoMapper;
using Domain.DTO_s.UserDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserServices;

public class UserService: IUserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetUsersDto>>> GetUsersAsync(UserFilter filter)
    {
        try
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                users = users.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.RegistrationDate.ToString()))
                users = users.Where(x =>
                    x.RegisterDate.ToString().ToLower().Contains(filter.RegistrationDate.ToString().ToLower()));
            if (!string.IsNullOrEmpty(filter.Role))
                users = users.Where(e => e.Role.ToLower().Contains(filter.Role.ToLower()));
            var result = await users.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await users.CountAsync();
            var response = _mapper.Map<List<GetUsersDto>>(result);
            return new PagedResponse<List<GetUsersDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUsersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetUsersDto>> GetUserByIdAsync(int userId)
    {
        try
        {

            var exist = await _context.Users.FindAsync(userId);
            if (exist == null) return new Response<GetUsersDto>(HttpStatusCode.NotFound, "User Not Found!");

            var mapped = _mapper.Map<GetUsersDto>(exist);
            return new Response<GetUsersDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetUsersDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddUserAsync(AddUserDto AddUser)
    {
        try
        {
            if (AddUser == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<User>(AddUser);
            await _context.Users.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdateUserAsync(UpdateUserDto update)
    {
        try
        {
            var existing = await _context.Users.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<User>(update);
            _context.Users.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserAsync(int userId)
    {
        try
        {
            var existing = await _context.Users.Where(e => e.Id == userId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"User not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}