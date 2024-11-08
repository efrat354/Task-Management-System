﻿namespace Dal;
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
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    //Delete an task by its id only if there is not task that depends on it
    public void Delete(int id)
    {
        Task? reference = Read(id);
        if (reference != null)
        {
            DataSource.Tasks.Remove(reference);
        }
        else
        {
            throw new DalDoesNotExistException("The item to delete does not exist in the system");
        }
    }
    //Gets a pointer to a boolean function which will go through the task's list and return the first task in the list on which the function returns True.
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter!);
    }

    //Read the task's details by its id-find it in the tasks' list and return a reference
    public Task? Read(int id)
    {
        return (DataSource.Tasks).FirstOrDefault(task => task?.Id == id);
    }

    //Gets a pointer to a boolean function ,which will go through the task's list and return the list of all tasks objects in the list for which the function returns True. If no pointer is sent the entire list will be returned.
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

    //Delete all the list's data
    public void Reset()
    {
        if (DataSource.Tasks.Count != 0)
        {
            DataSource.Tasks.Clear();
        }
    }
}
