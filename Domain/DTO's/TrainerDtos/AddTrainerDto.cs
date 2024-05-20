namespace Domain.DTO_s.TrainerDtos;

public class AddTrainerDto
{
    public required string  Name { get; set; }
    public string Email { get; set; }
    public required string Phone { get; set; }
    public string Specialization { get; set; }
    public string? Photo { get; set; }

}