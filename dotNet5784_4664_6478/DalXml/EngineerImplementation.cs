namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    const string engineerFile = @"..\xml\engineers.xml";
    public int Create(Engineer item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(engineerFile))
        {
            serializer.Serialize(writer, lst);
        }

        return item.Id;
    }

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

    public Engineer? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return lst.FirstOrDefault(engineer => engineer?.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return lst.FirstOrDefault(filter!);

    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        List<Engineer> lst = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return lst.Select(item => item);
        else
            return lst.Where(filter);
    }

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

