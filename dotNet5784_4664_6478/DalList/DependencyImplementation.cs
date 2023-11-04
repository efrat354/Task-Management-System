
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)//Add a new item to the dependency's list
    {
        int id = DataSource.Config.NextDependencyId;//Give the next continuous number as an id to the item.
        Dependency copy = item with { Id = id };//create a new item
        DataSource.Dependencies.Add(copy);//Add the new item to the list
        return id;
    }

    public void Delete(int id)
    {
        Dependency? reference = Read(id);
        if (reference != null)
        {
            DataSource.Dependencies.Remove(reference);
        }
        else
        {
            throw new Exception("The dependency to delete does not exist in the system");
        }
    }

    //Recieves an ID and check if it exists in the list 
    public Dependency? Read(int id)
    {
        if (DataSource.Dependencies.Exists(x => x!.Id == id))//
        {
            return DataSource.Dependencies.Find(x => x!.Id == id);
        }
        return null;
    }

    public List<Dependency> ReadAll()
    {
        //List<Dependency> newList = new List<Dependency>(DataSource.Dependencies.Count);

        //DataSource.Dependencies.ForEach((item) =>
        //{
        //    newList.Add(item!);
        //});
        //return newList;
        return new List<Dependency>(DataSource.Dependencies!);
    }

    public void Update(Dependency item)
    {
        Dependency? reference = Read(item.Id);
        if (reference!=null)
        {
            DataSource.Dependencies.Remove(reference);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new Exception("The item to update does not exist in the system");
        }
    }
}
