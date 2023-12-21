namespace BO;

public class Task
{
    public int Id { get; init; }
    public string ?Alias { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public Status Status { get; set; }
    public List<TaskInList> ?Dependencies { get; set; }
    public Milestone ?Milestone { get; set; }
    public DateTime ?ScheduledStartDate { get; set; }
    public DateTime ?StartDate { get; set;}
    public DateTime? ScheduledEndDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Product { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask ?Engineer { get; set; }
    public EngineerExperience Level { get; set; }
   // TimeSpan RequiredEffortTime = new TimeSpan(),
}
