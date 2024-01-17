namespace BO;
/// <summary>
/// A class that describe an engineer
/// </summary>
public class Engineer
{
    public int Id { get; init; }
    public required string  Name { get; set; }
    public required string Email { get; set; }
    public required EngineerExperience Level { get; set; }//לשאול את המורה האם לעשות required
    public required double Cost { get; set; }
    public TaskInEngineer? Task { get; set; }
    public override string ToString() => this.genericToString();
}

