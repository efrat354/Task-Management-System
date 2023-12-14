namespace Dal;
using DalApi;
using DO;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{
    const string taskFile = @"..\xml\tasks.xml";

    public int Create(Task item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        lst.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(lst, "tasks");
        return item.Id;
    }

    public void Delete(int id)
    {
        Task? reference = Read(id);
        if (reference == null)
        {
            throw new DalDoesNotExistException("Task does not exist, cannot be deleted");
        }
        else
        {
            Task Task = reference with { Active = false };
            Update(Task);
        }
    }

    public Task? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return lst.FirstOrDefault(Task => Task?.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return lst.FirstOrDefault(filter!);
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        List<Task> lst = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return lst.Select(item => item);
        else
            return lst.Where(filter);
    }

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
}
