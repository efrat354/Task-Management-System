using DO;
namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new dependency object
    T? Read(int id); //Reads a dependency  by its ID 
    List<T> ReadAll();
    //IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); 
    void Update(T item); //Updates dependency object
    void Delete(int id); //Deletes a dependency by its Id
}
