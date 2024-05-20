using System.Net;
using AutoMapper;
using Domain.DTO_s.PaymentDtos;
using Domain.Filters;
using Domain.Models;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PaymentServices;

public class PaymentService:IPaymentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PaymentService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetPaymentDto>>> GetPaymentsAsync(PaymentFilter filter)
    {
        try
        {
            var Payments = _context.Payments.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Date.ToString()))
                Payments = Payments.Where(e => e.Date.ToString().ToLower().Contains(filter.Date.ToString().ToLower()));

            
            var result = await Payments.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Payments.CountAsync();
            var response = _mapper.Map<List<GetPaymentDto>>(result);
            return new PagedResponse<List<GetPaymentDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPaymentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetPaymentDto>> GetPaymentByIdAsync(int PaymentId)
    {
        try
        {

            var exist = await _context.Payments.FindAsync(PaymentId);
            if (exist == null) return new Response<GetPaymentDto>(HttpStatusCode.NotFound, "Payment Not Found!");

            var mapped = _mapper.Map<GetPaymentDto>(exist);
            return new Response<GetPaymentDto>(mapped);  
        }
        catch (Exception e)
        {
            return new PagedResponse<GetPaymentDto>(HttpStatusCode.InternalServerError, e.Message);

        }
        
    }

    public async Task<Response<string>> AddPaymentAsync(AddPaymentDto AddPayment)
    {
        try
        {
            if (AddPayment == null)
            {
                return new Response<string>("Error!");
            }
            
            var mapped = _mapper.Map<Payment>(AddPayment);
            await _context.Payments.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Added successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
        
    }

    public async Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto update)
    {
        try
        {
            var existing = await _context.Payments.AnyAsync(e => e.Id == update.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found!");
            var mapped = _mapper.Map<Payment>(update);
            _context.Payments.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<bool>> DeletePaymentAsync(int PaymentId)
    {
        try
        {
            var existing = await _context.Payments.Where(e => e.Id == PaymentId).ExecuteDeleteAsync();
            if ( existing == 0)return new Response<bool>(HttpStatusCode.BadRequest,"Payment not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }    }
}