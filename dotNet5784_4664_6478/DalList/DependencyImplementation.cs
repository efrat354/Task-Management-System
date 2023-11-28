namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
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
            throw new DalDoesNotExistException("The dependency to delete does not exist in the system");
        }
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
         return DataSource.Dependencies.FirstOrDefault(filter!);
    }

    //Gets ID and check if it exists in the list 
    public Dependency? Read(int id)
    {
        return (DataSource.Dependencies).FirstOrDefault(dependency => dependency?.Id == id);
    }

    //Return the dependencies list
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }
    //Gets a dependency and update the dependency with the same ID from the dependencies list 
    public void Update(Dependency item)
    {
        Dependency? reference = Read(item.Id);
        if (reference != null)
        {

            DataSource.Dependencies.Remove(reference);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new DalDoesNotExistException("The dependency to update does not exist in the system");
        }
    }
}
