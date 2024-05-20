using Domain.Enums;

namespace Domain.DTO_s.WorkoutDtos;

public class AddWorkoutDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Intensity Intensity { get; set; }

}