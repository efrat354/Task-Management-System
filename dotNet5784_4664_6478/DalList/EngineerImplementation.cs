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
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    //Delete an engineer by his id- change the engineer to inactive
    public void Delete(int id)
    {
        Engineer? reference = Read(id);
        if (reference==null)
        {
            throw new Exception("Cannot be deleted, engineer does not exist");
        }
        else
        {
           Engineer engineer = reference with { status=false};
            Update(engineer);
        }
        
    }

    //Read the engineer's details by his id-find him in the engineers' list and return a reference
    public Engineer? Read(int id)
    {
        var eng = (DataSource.Engineers).Where(engineer => engineer?.Id == id);
        if(eng!=null)
        {
            return (Engineer)eng;
        }
        return null;
        //if (DataSource.Engineers.Exists(x => x!.Id == id))
        //{
        //    return DataSource.Engineers.Find(x => x!.Id == id);
        //}
        //return null;
    }

    //Read all the engineers' list-return a new list that include all the details
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers!);
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
            throw new Exception("The item to update does not exist in the system");
        }
    }
}
