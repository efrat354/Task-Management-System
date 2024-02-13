namespace BO;
/// <summary>
/// An entity that describe MilestoneInList
/// </summary>
/// <param name="Id">Milestone ID number</param>
/// <param name="Alias">A alias for the milestone</param>
/// <param name="Description">A milestone's description</param>
/// <param name="CreatedAtDate">Milestone's creation date</param>
/// <param name="Status"> Current status of a milestone </param>
/// <param name="ScheduledStartDate">Milestone's estimated date of completion</param>
/// <param name="StartDate">Milestone's start date</param>
/// <param name="DeadlineDate">Milestone's final date for completion</param>
/// <param name="CompleteDate">Milestone's actual end date</param>
/// <param name="CompletionPercentage">Percentage of completion of the tasks in the milestone</param>
/// <param name="Remarks">Remarks on the milestone</param>
/// <param name="Dependencies">List of tasks that depend on millstone</param>
/// <param name="ToString">Print the entity as a string</param>

public class Milestone
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public DateTime? ScheduledStartDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public override string ToString() => this.GenericToString();
}
