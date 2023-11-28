using DO;
namespace DalApi;
// global interface for the different entities
public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new entity object
    T? Read(int id); //Reads an entity by its ID 
    T? Read(Func<T, bool> filter); //Reads an entity by a filter
    IEnumerable<T?> ReadAll(Func<T?, bool>? filter = null);//return all the objects in the entity by a filter
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes a entity by its Id
}
