namespace DalApi;
using DO;
//Engineer interface
public interface IEngineer
{
    int Create(Engineer item); //Creates new engineer object 
    Engineer? Read(int id); //Reads engineer object by its ID 
    List<Engineer> ReadAll(); //Reads all the engineers objects 
    void Update(Engineer item); //Updates engineer object
    void Delete(int id); //Deletes a engineer by its Id

}
