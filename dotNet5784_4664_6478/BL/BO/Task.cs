namespace BO;

public class Task
{
    /*    
    BaselineStartDate 
    StartDate 
    ScheduledDate 
    ForecastDate 
    DeadlineDate 
    CompleteDate 
    Deliverables 
    Remarks 
    double RequiredEffort { get; set; }
    public Tuple<string, string>? Engineer { get; set; }
    public EngineerExperience ComplexityLevel { get; set; } =               EngineerExperience.None;

    public override string? ToString() => base.ToString();
*/
    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public Milestone? Milestone { get; set; }
    public DateTime? ScheduledStartDate { get; set; }//הוא כתב baselinedate מה צריך לעשות עם התאריך הזה
    public DateTime? StartDate { get; set; }
    public DateTime? ForecastDate { get; set; }//ScheduledEndDate
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Product { get; set; }
    public string? Remarks { get; set; }
    public Tuple<string, string>? Engineer { get; set; }//????
    public EngineerExperience Level { get; set; }
    // public override string? ToString() => base.ToString();

    // TimeSpan RequiredEffortTime = new TimeSpan(),
}