

namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null);//האם צריך TaskInList?
    public BO.Task Read(int id);
    public void Create(BO.Task task);
    public void Delete(int id);
    public void Update(BO.Task task);

}
