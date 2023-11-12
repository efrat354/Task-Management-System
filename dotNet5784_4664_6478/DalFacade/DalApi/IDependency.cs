namespace DalApi;
using DO;
//Dependency interface
public interface IDependency
{
    int Create(Dependency item); //Creates a new dependency object
    Dependency? Read(int id); //Reads a dependency  by its ID 
    List<Dependency> ReadAll(); //Reads all the dependencies objects 
    void Update(Dependency item); //Updates dependency object
    void Delete(int id); //Deletes a dependency by its Id
}
