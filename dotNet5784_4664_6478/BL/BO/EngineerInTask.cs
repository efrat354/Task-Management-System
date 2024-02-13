namespace BO;
/// <summary>
/// A class that describe an engineer who responsible on a task  
/// </summary>
/// <param name="Id">Engineer's ID</param>
/// <param name="Name">Engineer's name</param>
/// /// <param name="ToString">Print the entity as a string</param>
public class EngineerInTask
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public override string ToString() => this.GenericToString();
}
