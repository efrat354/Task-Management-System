namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public string? Email { get; set; }//כנ"ל
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public bool Active { get; set; }//לבדוק אם היינו צריכות את זה
    public TaskInEngineer? Task { get; set; }
    //public override string ToString() => this.ToStringProperty();
}

