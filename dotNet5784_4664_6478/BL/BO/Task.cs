namespace BO;
/// <summary>
/// An entity that describe Task
/// </summary>
/// <param name="Id">Task ID number</param>
/// <param name="Alias">A alias for the task</param>
/// <param name="Description">A task's description</param>
/// <param name="CreatedAtDate">Task's creation date</param>
/// <param name="Status"> Current status of a task </param>
/// <param name="Dependencies">Tasks that the task depends on</param>
/// <param name="Milestone">The task's milestone</param>
/// <param name="ScheduledDate">Task's estimated date of completion</param>
/// <param name="StartDate">Task's start date</param>
///  <param name="RequiredEffortTime">The time required for the operation</param>
/// <param name="DeadlineDate">Task's final date for completion</param>
/// <param name="CompleteDate">Task's actual end date</param>
/// <param name="Product">Product -describing the product</param>
/// <param name="Remarks">Remarks on the task</param>
/// <param name="EngineerId">The engineer's id the responsible on the task</param>
/// <param name="ComplexityLevel">Task's status</param>
/// <param name="ToString">Print the entity as a string</param>
 

public class Task
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public DateTime? ScheduledStartDate { get; set; }
    public DateTime? StartDate { get; set; }
    public TimeSpan RequiredEffortTime { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Product { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience ComplexityLevel { get; set; }
    public override string ToString() => this.GenericToString();
}