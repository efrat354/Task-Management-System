namespace DO;
/// <summary>
/// An entity that describe task
/// </summary>
/// <param name="Id">indicates the time when the task was created by the administrator</param>
/// <param name="Alias">A alias for the task</param>
/// <param name="Description">A task's description</param>
/// <param name="CreatedAtDate">Task's creation date</param>
/// <param name="RequiredEffortTime">Work start date, calculated in such a way that, according to the available information, all tasks will be carried out until the time of the end of the project</param>
/// <param name="IsMilestone">Milestones during the mission</param>
/// <param name="Complexity">The level of complexity of the task</param>
/// <param name="StartDate">Task's start date</param>
/// <param name="ScheduledDate">Task's estimated date of completion</param>
/// <param name="DeadlineDate">Task's final date for completion</param>
/// <param name="CompleteDate">Task's actual end date</param>
/// <param name="Product">Product -describing the product</param>
/// <param name="Remarks">Remarks on the task</param>
/// <param name="EngineerId">The engineer's id the responsible on the task</param>
/// <param name="Active">Task's status</param>
/// 

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
    int? EngineerId =null,
    bool Active=true
)
{
    public Task() : this(0) { }
}
