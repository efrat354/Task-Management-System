
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        if (DataSource.Tasks.Exists(x => x!.Id == id))
        {
            return DataSource.Tasks.Find(x => x!.Id == id);
        }
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks!);
    }

    public void Update(Task item)
    {
        Task? reference = Read(item.Id);
        if (reference != null)
        {
            DataSource.Tasks.Remove(reference);
            DataSource.Tasks.Add(item);
        }
        else
        {
            throw new Exception("The item to update does not exist in the system");
        }
    }
}
