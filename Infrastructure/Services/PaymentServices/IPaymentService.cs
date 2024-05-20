using Domain.DTO_s.PaymentDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PaymentServices;

public interface IPaymentService
{
    Task<PagedResponse<List<GetPaymentDto>>> GetPaymentsAsync(PaymentFilter filter);
    Task<Response<GetPaymentDto>> GetPaymentByIdAsync(int PaymentId);
    Task<Response<string>> AddPaymentAsync(AddPaymentDto AddPayment);
    Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto update);
    Task<Response<bool>> DeletePaymentAsync(int PaymentId);

}