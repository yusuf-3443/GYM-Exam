namespace Domain.Filters;

public class TrainerFilter:PaginationFilter
{
    public string? Specialization { get; set; }
    public string? Name { get; set; }
}