namespace BO;

public class TaskInList
{
    public int Id { get; init; }
    public string Alias { get; set; }//למה לא נותן להיות NN
    public string Description { get; set; }//
    public Status Status { get; set; }
}
