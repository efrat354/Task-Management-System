namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

/// <summary>
/// The implementation of the engineer's CRUD functions 
/// </summary>
internal class EngineerImplementation : IEngineer
{
    //Create a new engineer and add it to the engineers' list 
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    //Delete an engineer by his id- change the engineer to inactive
    public void Delete(int id)
    {
        Engineer? reference = Read(id);
        if (reference == null)
        {
            throw new DalDoesNotExistException("Engineer does not exist, cannot be deleted");
        }
        else
        {
            Engineer engineer = reference with { Active = false };
            Update(engineer);
        }

    }
    //Gets a pointer to a boolean function which will go through the engineer's list and return the first engineer in the list on which the function returns True.
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter!);
    }

    //Read the engineer's details by his id-find him in the engineers' list and return a reference
    public Engineer? Read(int id)
    {
        return (DataSource.Engineers).FirstOrDefault(engineer => engineer?.Id == id);

    }

    //Gets a pointer to a boolean function ,which will go through the engineer's list and return the list of all the engineers objects in the list for which the function returns True. If no pointer is sent the entire list will be returned.

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    //Delete all the list's data
    public void Reset()
    {
        if (DataSource.Engineers.Count != 0)
        {
            DataSource.Engineers.Clear();
        }
    }

    //Update the engineer's details by his id
    public void Update(Engineer item)
    {
        Engineer? reference = Read(item.Id);
        if (reference != null)
        {
            DataSource.Engineers.Remove(reference);
            DataSource.Engineers.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException("The item to update does not exist in the system");
        }
    }
}
