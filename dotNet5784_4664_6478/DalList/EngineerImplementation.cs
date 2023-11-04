
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //for entities with normal id (not auto id)
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

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

    public Engineer? Read(int id)
    {
        if (DataSource.Engineers.Exists(x => x!.Id == id))//
        {
            return DataSource.Engineers.Find(x => x!.Id == id);
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers!);
    }

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
