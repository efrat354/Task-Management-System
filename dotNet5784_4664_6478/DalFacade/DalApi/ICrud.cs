using DO;
namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new dependency object
    T? Read(int id); //Reads a dependency  by its ID 
    List<T> ReadAll(); //Reads all the dependencies objects 
    void Update(T item); //Updates dependency object
    void Delete(int id); //Deletes a dependency by its Id
}
