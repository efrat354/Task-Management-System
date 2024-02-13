namespace BO;
/// <summary>
/// A class that describe an engineer
/// /// <summary>
/// An entity that describe engineer
/// </summary>
/// <param name="Id">Engineer's ID</param>
/// <param name="Name">Engineer's name</param>
/// <param name="Email">Engineer's email</param>
/// <param name="Level">The level of experience of the engineer</param>
/// <param name="Cost">The cost of the engineer per hour</param>
/// <param name="Task">The engineer's current task</param>
/// <param name="ToString">Print the entity as a string</param>
public class Engineer
{
    public int Id { get; init; }
    public required string  Name { get; set; }
    public required string Email { get; set; }
    public required EngineerExperience Level { get; set; }//לשאול את המורה האם לעשות required
    public required double Cost { get; set; }
    public TaskInEngineer? Task { get; set; }
    public override string ToString() => this.GenericToString();
}