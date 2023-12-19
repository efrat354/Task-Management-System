namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
   /// The implementation of the engineer's CRUD functions 

    const string engineerFile = @"..\xml\engineers.xml";
    //Create a new engineer and add it to the engineer' xml file

    public int Create(Engineer item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        lst.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(lst, "engineers");
        return item.Id;
    }
    //Delete an engineer by his id- change the engineer to inactive

    public void Delete(int id)
    {
        Engineer? reference = Read(id);
        if (reference == null)
        {
            throw new DalDoesNotExistException("Engineer does not exist, cannot be deleted");
        }
        else
        {
            Engineer engineer = reference with { Active = false };
            Update(engineer);
        }
    }
    //Gets a pointer to a boolean function which will go through the engineer' xml file and return the first engineer in the list on which the function returns True.

    public Engineer? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return lst.FirstOrDefault(engineer => engineer?.Id == id);
    }
    //Read the engineer's details by his id-find him in the engineers' xml file and return a reference

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return lst.FirstOrDefault(filter!);

    }
    //Gets a pointer to a boolean function ,which will go through the engineers'xml file and return the list of all the engineers objects in the list for which the function returns True. If no pointer is sent the entire list will be returned.

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return lst.Select(item => item);
        else
            return lst.Where(filter);
    }
    //Update the engineer's details by his id

    public void Update(Engineer item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? reference = Read(item.Id);
        if (reference != null)
        {
            lst.Remove(reference);
            lst.Add(item);
            XMLTools.SaveListToXMLSerializer<Engineer>(lst, "engineers");
        }
        else
        {
            throw new DalDoesNotExistException("The item to update does not exist in the system");
        }
    }
}

