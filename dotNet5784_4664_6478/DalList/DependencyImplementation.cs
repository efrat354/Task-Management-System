namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
//Implementation of the dependency interface
internal class DependencyImplementation : IDependency
{//Gets a dependency ,Create a copy of a dependency and add it to the dependencies list
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }
    //Gets ID and deleting the dependency with the Received ID from the list
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

    //Gets ID and check if it exists in the list 
    public Dependency? Read(int id)
    {
        var dep = (DataSource.Dependencies).Where(dependency => dependency?.Id == id);
        if (dep != null )
        {
            return (Dependency)dep;
        }
        return null;

        //if (DataSource.Dependencies.Exists(x => x!.Id == id))//
        //{
        //    return DataSource.Dependencies.Find(x => x!.Id == id);
        //}
        //return null;
    }
    //Return the dependencies list
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies!);
    }
    //Gets a dependency and update the dependency with the same ID from the dependencies list 
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
