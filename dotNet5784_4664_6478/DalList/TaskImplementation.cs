namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// The implementation of the task's CRUD functions 
/// </summary>
internal class TaskImplementation : ITask
{
    //Create a new task and add it to the tasks' list 
    public int Create(Task item)
    {
        EngineerImplementation e = new EngineerImplementation();
        Engineer? eng;
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        eng = e.Read(item.EngineerId);
        if (eng!.status)
        {
            DataSource.Tasks.Add(copy);
            return id;
        }
        else
            throw new Exception("The engineer is not active");
    }

    //Delete an task by its id only if there is not task that depends on it
    public void Delete(int id)
    {
        Task? reference = Read(id);
        if (reference != null)
        {
            foreach (Dependency? depend in DataSource.Dependencies)
            {
                if (depend?.DependentTask == id)
                {
                    throw new Exception("This task cannot be deleted because other tasks depend on it");
                }
            }
            foreach (Dependency? depend in DataSource.Dependencies)
            {
                if (depend?.DependsOnTask == id)
                {
                    DataSource.Dependencies.Remove(depend);
                }
            }
            DataSource.Tasks.Remove(reference);
        }
        else
        {
            throw new Exception("The item to delete does not exist in the system");
        }
    }

    //Read the task's details by its id-find it in the tasks' list and return a reference
    public Task? Read(int id)
    {
        var tk = (DataSource.Tasks).Where(task => task?.Id == id);
        if (tk != null)
        {
            return (Task)tk;
        }
        return null;
        //if (DataSource.Tasks.Exists(x => x!.Id == id))
        //{
        //    return DataSource.Tasks.Find(x => x!.Id == id);
        //}
        //return null;
    }

    //Read all the tasks' list-return a new list that include all the details
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks!);
    }

    //Update the task's details by its id
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
