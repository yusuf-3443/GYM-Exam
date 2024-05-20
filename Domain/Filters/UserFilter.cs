namespace Domain.Filters;

public class UserFilter:PaginationFilter
{
    public string? Name { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? Role { get; set; }
}