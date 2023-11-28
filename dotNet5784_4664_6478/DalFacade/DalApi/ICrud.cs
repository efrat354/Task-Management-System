using DO;
namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new dependency object
    //List<T> ReadAll(); //Reads all the dependencies objects 
    T? Read(int id); //Reads a dependency  by its ID 
    T? Read(Func<T, bool> filter); // stage 2
    IEnumerable<T?> ReadAll(Func<T?, bool>? filter = null); // stage 2
    void Update(T item); //Updates dependency object
    void Delete(int id); //Deletes a dependency by its Id
}
