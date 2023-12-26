namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public required string Alias { get; set; }//למה לא נותן להיות NN
    public required string Description { get; set; }//
    public Status Status { get; set; }
}
