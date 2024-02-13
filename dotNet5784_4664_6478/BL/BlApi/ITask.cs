

namespace BlApi;
/// <summary>
/// Task interface
/// </summary>

public interface ITask
{ 
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task?, bool>? filter = null);
    public BO.Task Read(int id);
    public void Delete(int id);  
    public void Update(BO.Task task);

}
