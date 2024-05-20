namespace Domain.Models;

public class ClassSchedule
{

    public int Id { get; set; }
    public int WorkOutId { get; set; }
    public int TrainerId { get; set; }
    public DateTime DateTime { get; set; }
    public int Duration { get; set; }
    public string Location { get; set; }

    public WorkOut? WorkOut { get; set; }
    public Trainer? Trainer { get; set; }
}