using Domain.Enums;

namespace Domain.DTO_s.PaymentDtos;

public class AddPaymentDto
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }

}