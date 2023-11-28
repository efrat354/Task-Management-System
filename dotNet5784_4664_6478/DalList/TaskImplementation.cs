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
        if (eng!.Active)
        {
            DataSource.Tasks.Add(copy);
            return id;
        }
        else
            throw new DalInvalidInitialization("The engineer is not active,cannot be created");
    }

    //Delete an task by its id only if there is not task that depends on it
    public void Delete(int id)
    {
        Task? reference = Read(id);
        if (reference != null)
        {
            var dependency= (DataSource.Dependencies).FirstOrDefault(depend => depend?.DependentTask == id);
            if (dependency != null)
            {
                throw new DalDeletionImpossible("This task cannot be deleted because other tasks depend on it");
            }
            else
            {
               // DataSource.Dependencies.Remove(dependency);
            }
            Task task = reference with { Active = false };
            Update(task);
        }
        else
        {
            throw new DalDeletionImpossible("The item to delete does not exist in the system");
        }
        //foreach (Dependency? depend in DataSource.Dependencies)
        //{
        //    if (depend?.DependentTask == id)
        //    {
        //        throw new Exception("This task cannot be deleted because other tasks depend on it");
        //    }
        //}
        //foreach (Dependency? depend in DataSource.Dependencies)
        //{
        //    if (depend?.DependsOnTask == id)
        //    {
        //        DataSource.Dependencies.Remove(depend);
        //    }
        //}
        //DataSource.Tasks.Remove(reference);
    }
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter!);
    }

    //Read the task's details by its id-find it in the tasks' list and return a reference
    public Task? Read(int id)
    {
        return (DataSource.Tasks).FirstOrDefault(task => task?.Id == id);
    }

    //Read all the tasks' list-return a new list that include all the details

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
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
            throw new DalDoesNotExistException("The item to update does not exist in the system");
        }
    }
}
