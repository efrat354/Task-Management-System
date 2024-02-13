
namespace BO;
/// <summary>
/// An entity that describe Task In List
/// </summary>
/// <param name="Id">Task ID number</param>
/// <param name="Alias">A alias for the task</param>
/// <param name="ToString">Print the entity as a string</param>

public class TaskInList
{

    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public Status? Status { get; set; }
    public override string ToString() => this.GenericToString();
}
