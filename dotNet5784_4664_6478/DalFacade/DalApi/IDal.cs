namespace DalApi;
public interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }
    //DateTime? StartProjectDate { get; }
    //DateTime? EndProjectDate { get; }
   // public void Reset();
   
}
