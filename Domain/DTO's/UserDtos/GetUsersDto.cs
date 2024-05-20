namespace Domain.DTO_s.UserDtos;

public class GetUsersDto
{
    public int  Id { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime RegisterDate { get; set; }
    public string Role { get; set; }

}