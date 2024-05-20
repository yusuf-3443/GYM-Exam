using Domain.Enums;

namespace Domain.DTO_s.MembershipDtos;

public class AddMembershipDto
{
    public int UserId { get; set; }
    public MembershipType Type { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}