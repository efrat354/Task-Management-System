namespace BO;
/// <summary>
/// An entity that describe Milestone In Task
/// </summary>
/// <param name="Id">Milestone ID number</param>
/// <param name="Alias">A alias for the milestone</param>
/// <param name="ToString">Print the entity as a string</param>

public class MilestoneInTask
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => this.GenericToString();
}
