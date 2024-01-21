namespace Dal;
using DalApi;
using DO;
using System.Xml.Serialization;
/// <summary>
/// The implementation of the task's CRUD functions 
/// </summary>
internal class TaskImplementation : ITask
{
    const string taskFile = @"..\xml\tasks.xml";
    //Create a new task and add it to the tasks' xml file 

    public int Create(Task item)
    {
        int newTaskId = Config.NextTaskId;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task copy = item with { Id = newTaskId };
        lst.Add(copy);
        XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        return copy.Id;
    }
    //Delete an task by its id only if there is not task that depends on it
    public void Delete(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? reference = Read(id);
        if (reference != null)
        {
            lst.Remove(reference);
            XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        }
        else
        {
             throw new DalDoesNotExistException("Task does not exist, cannot be deleted");
        }
    }
    //Read the task's details by its id-find it in the tasks' xml file and return a reference
    public Task? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return lst.FirstOrDefault(Task => Task?.Id == id);
    }
    //Gets a pointer to a boolean function which will go through the task's list and return the first task in the xml file on which the function returns True.
    public Task? Read(Func<Task, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return lst.FirstOrDefault(filter!);
    }
    //Gets a pointer to a boolean function ,which will go through the task's xml file and return the xml file of all tasks objects in the xml file for which the function returns True. If no pointer is sent the entire xml file will be returned.
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return lst.Select(item => item);
        else
            return lst.Where(filter);
    }
    //Update the task's details by its id
    public void Update(Task item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? reference = Read(item.Id);
        if (reference != null)
        {
            lst.Remove(reference);
            lst.Add(item);
            XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        }
        else
        {
            throw new DalDoesNotExistException("The item to update does not exist in the system");
        }
    }
    //Delete all the xml's data
    public void Reset()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (lst.Count() != 0)
        {
            lst.Clear();
            XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        }
    }
}
