using System.Net;
using AutoMapper;
using Domain.DTO_s.TrainerDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TrainerServices;

public class TrainerService:ITrainerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TrainerService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetTrainersDto>>> GetTrainersAsync(TrainerFilter filter)
    {
        try
        {
            var Trainers = _context.Trainers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                Trainers = Trainers.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrEmpty(filter.Specialization))
                Trainers = Trainers.Where(e => e.Specialization.ToLower().Contains(filter.Specialization.ToLower()));

            
            var result = await Trainers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Trainers.CountAsync();
            var response = _mapper.Map<List<GetTrainersDto>>(result);
            return new PagedResponse<List<GetTrainersDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTrainersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTrainersDto>> GetTrainerByIdAsync(int TrainerId)
    {
        try
        {

            var exist = await _context.Trainers.FindAsync(TrainerId);
            if (exist == null) return new Response<GetTrainersDto>(HttpStatusCode.NotFound, "Trainer Not Found!");

            var mapped = _mapper.Map<GetTrainersDto>(exist);
            return new Response<GetTrainersDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetTrainersDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddTrainerAsync(AddTrainerDto AddTrainer)
    {
        try
        {
            if (AddTrainer == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<Trainer>(AddTrainer);
            await _context.Trainers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto update)
    {
        try
        {
            var existing = await _context.Trainers.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<Trainer>(update);
            _context.Trainers.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<bool>> DeleteTrainerAsync(int TrainerId)
    {
        try
        {
            var existing = await _context.Trainers.Where(e => e.Id == TrainerId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Trainer not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}