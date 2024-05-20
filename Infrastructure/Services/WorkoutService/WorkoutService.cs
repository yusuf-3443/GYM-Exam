using System.Net;
using AutoMapper;
using Domain.DTO_s.WorkoutDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.WorkoutService;

public class WorkoutService:IWorkoutService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WorkoutService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkoutsAsync(WorkoutFilter filter)
    {
        try
        {
            var Workouts = _context.Workouts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Duration.ToString()))
                Workouts = Workouts.Where(e => e.Duration.ToString().ToLower().Contains(filter.Duration.ToString().ToLower()));
            
            var result = await Workouts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Workouts.CountAsync();
            var response = _mapper.Map<List<GetWorkoutsDto>>(result);
            return new PagedResponse<List<GetWorkoutsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetWorkoutsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetWorkoutsDto>> GetWorkoutByIdAsync(int WorkoutId)
    {
        try
        {

            var exist = await _context.Workouts.FindAsync(WorkoutId);
            if (exist == null) return new Response<GetWorkoutsDto>(HttpStatusCode.NotFound, "Workout Not Found!");

            var mapped = _mapper.Map<GetWorkoutsDto>(exist);
            return new Response<GetWorkoutsDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetWorkoutsDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddWorkoutAsync(AddWorkoutDto AddWorkout)
    {
        try
        {
            if (AddWorkout == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<WorkOut>(AddWorkout);
            await _context.Workouts.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto update)
    {
        try
        {
            var existing = await _context.Workouts.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<WorkOut>(update);
            _context.Workouts.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteWorkoutAsync(int WorkoutId)
    {
        try
        {
            var existing = await _context.Workouts.Where(e => e.Id == WorkoutId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Workout not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}