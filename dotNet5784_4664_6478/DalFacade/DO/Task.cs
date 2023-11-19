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
    string Description=" ",
    string? Alias=null,
    bool Milestone=false,
    DateTime CreatedAt = new DateTime(),
    DateTime? Start = null,
    DateTime? ForcastDate = null,
    DateTime? Deadline = null,
    DateTime? Complete = null,
    string? Product=null,
    string? Remarks = null,
    int EngineerId = 0,
    EngineerExperience ComplexityLevel=0
)
{
    public Task() : this(0) { }
}
