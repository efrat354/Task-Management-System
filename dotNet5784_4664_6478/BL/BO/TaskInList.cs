namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public required string Description { get; set; }
    public Status? Status { get; set; }
    public override string ToString() => this.GenericToString();
}
