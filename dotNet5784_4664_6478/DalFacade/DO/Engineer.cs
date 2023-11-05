namespace DO;
public record Engineer
(
    int Id,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost,
    bool status = true 
)
{
  public Engineer() : this(0,"","",EngineerExperience.Rookie,0) { } //empty ctor 

}
