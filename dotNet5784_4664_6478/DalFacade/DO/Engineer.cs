namespace DO;
/// <summary>
/// An entity that describe engineer
/// </summary>
/// <param name="Id">Engineer's ID</param>
/// <param name="Name">Engineer's name</param>
/// <param name="Email">Engineer's email</param>
/// <param name="Level">The level of experience of the engineer</param>
/// <param name="Cost">The cost of the engineer per hour</param>
/// <param name="Active">Engineer's Active- if the engineer is active</param>
public record Engineer
(
    int Id=0,
    string Name="",
    string Email = "",
    EngineerExperience Level=0,
    double Cost = 30,
    bool Active = true 
)
{
  public Engineer() : this(0,"","",EngineerExperience.Novice,0,true) { } //empty ctor 

}
