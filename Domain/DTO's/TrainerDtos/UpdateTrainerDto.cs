namespace Domain.DTO_s.TrainerDtos;

public class UpdateTrainerDto
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Specialization { get; set; }
    public string? Photo { get; set; }

}