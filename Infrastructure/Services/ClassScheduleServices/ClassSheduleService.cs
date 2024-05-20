using System.Net;
using AutoMapper;
using Domain.DTO_s.ClassSheduleDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClassSheduleServices;

public class ClassScheduleService:IClassScheduleService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClassScheduleService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassSchedulesAsync(ClassSheduleFilter filter)
    {
        try
        {
            var ClassShedules = _context.ClassShedules.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Location))
                ClassShedules = ClassShedules.Where(e => e.Location.ToLower().Contains(filter.Location.ToLower()));
            
            var result = await ClassShedules.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await ClassShedules.CountAsync();
            var response = _mapper.Map<List<GetClassSchedulesDto>>(result);
            return new PagedResponse<List<GetClassSchedulesDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetClassSchedulesDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassSchedulesDto>> GetClassScheduleByIdAsync(int ClassSheduleId)
    {
        try
        {

            var exist = await _context.ClassShedules.FindAsync(ClassSheduleId);
            if (exist == null) return new Response<GetClassSchedulesDto>(HttpStatusCode.NotFound, "ClassShedule Not Found!");

            var mapped = _mapper.Map<GetClassSchedulesDto>(exist);
            return new Response<GetClassSchedulesDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetClassSchedulesDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddClassScheduleAsync(AddClassScheduleDto addClassSchedule)
    {
        try
        {
            if (addClassSchedule == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<ClassSchedule>(addClassSchedule);
            await _context.ClassShedules.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto update)
    {
        try
        {
            var existing = await _context.ClassShedules.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<ClassSchedule>(update);
            _context.ClassShedules.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteClassSchedule(int ClassSheduleId)
    {
        try
        {
            var existing = await _context.ClassShedules.Where(e => e.Id == ClassSheduleId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"ClassShedule not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}