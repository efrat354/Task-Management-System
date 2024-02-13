namespace BO;
/// <summary>
/// An entity that describe Milestone In List
/// </summary>
/// <param name="Id">Milestone ID number</param>
/// <param name="Alias">A alias for the milestone</param>
/// <param name="Description">A milestone's description</param>
/// <param name="CreatedAtDate">milestone's creation date</param>
/// <param name="Status"> Current status of a milestone </param>
/// <param name="CompletionPercentage">Percentage of completion of the tasks in the milestone</param>
/// <param name="ToString">Print the entity as a string</param>

public class MilestoneInList
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public double? CompletionPercentage { get; set; }
    public override string ToString() => this.GenericToString();
}
