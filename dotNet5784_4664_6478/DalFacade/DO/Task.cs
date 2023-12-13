namespace DO;
/// <summary>
/// An entity that describe task
/// </summary>
/// <param name="Id">An id -continuous number of the task</param>
/// <param name="Description">A task's description</param>
/// <param name="Alias">A alias for the task</param>
/// <param name="Milestone">Milestones during the mission</param>
/// <param name="CreatedAt">Task's creation date</param>
/// <param name="Start">Task's start date</param>
/// <param name="ForcastDate">Task's estimated date of completion</param>
/// <param name="Deadline">Task's final date for completion</param>
/// <param name="Complete">Task's actual end date</param>
/// <param name="Product">Product -describing the product</param>
/// <param name="Remarks">Remarks on the task</param>
/// <param name="EngineerId">The engineer's id the responsible on the task</param>
/// <param name="ComplexityLevel">The level of complexity of the task</param>
public record Task
(
    int Id=0,
    string Alias = "",
    string Description="",
    DateTime CreatedAtDate = new DateTime(),
    TimeSpan RequiredEffortTime=new TimeSpan(), 
    bool IsMilestone =false,
    EngineerExperience Complexity = 0,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Product=null,
    string? Remarks = null,
    int EngineerId = 0,
    bool Active=true
)
{
    public Task() : this(0) { }
}
