using Domain;
using Domain.DTO_s.PaymentDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.PaymentServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IPaymentService context) : ControllerBase
{
    [HttpGet("get-Payments")]
    public async  Task<PagedResponse<List<GetPaymentDto>>> GetPaymentAsync([FromQuery]PaymentFilter filter)
    {
        return await context.GetPaymentsAsync(filter);
    }

    [HttpPost("create-Payment")]
    public async Task<Response<string>> AddPaymentAsync([FromForm]AddPaymentDto Payment)
    {
        return await context.AddPaymentAsync(Payment);
    }

    [HttpPut("update-Payment")]
    public async Task<Response<string>> UpdatePaymentAsync([FromForm]UpdatePaymentDto Payment)
    {
        return await context.UpdatePaymentAsync(Payment);
    }

    [HttpDelete("Delete Payment")]
    public async Task<Response<bool>> DeletePaymentAsync(int id)
    {
        return await context.DeletePaymentAsync(id);
    }

    [HttpGet("get-Payment-By-Id")]
    public async Task<Response<GetPaymentDto>> GetPaymentById(int id)
    {
        return await context.GetPaymentByIdAsync(id);
    }
}