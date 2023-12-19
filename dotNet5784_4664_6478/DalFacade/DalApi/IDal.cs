namespace DalApi;
/// <summary>
/// An interface includes the three sub-interfaces needed for the project 
/// as well as the general data of the project - the start and end date of the project 
/// as well as the Reset function
/// </summary>
public interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }
    DateTime? StartProjectDate { get; }
    DateTime? EndProjectDate { get; }
    public void Reset();
   
}
