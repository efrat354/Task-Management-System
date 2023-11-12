namespace DO;
public record Engineer
(
    int Id=0,
    string Name="",
    string Email = "",
    EngineerExperience Level=0,
    double Cost= 30,
    bool status = true 
)
{
  public Engineer() : this(0,"","",EngineerExperience.Rookie,0) { } //empty ctor 

}
