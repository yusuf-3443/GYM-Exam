using Domain.Enums;

namespace Domain.DTO_s.WorkoutDtos;

public class GetWorkoutsDto
{
    public int  Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Intensity Intensity { get; set; }

}