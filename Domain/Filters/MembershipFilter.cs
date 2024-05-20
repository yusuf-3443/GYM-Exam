namespace Domain.Filters;

public class MembershipFilter:PaginationFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Type { get; set; }
}