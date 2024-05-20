namespace Domain.DTO_s.ClassSheduleDtos;

public class GetClassSchedulesDto
{
    public int Id { get; set; }
    public int WorkOutId { get; set; }
    public int TrainerId { get; set; }
    public DateTime DateTime { get; set; }
    public int Duration { get; set; }
    public string Location { get; set; }

}