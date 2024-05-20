namespace Domain.Filters;

public class WorkoutFilter:PaginationFilter
{
    public int ? Duration { get; set; }
    public string? Title { get; set; }
}