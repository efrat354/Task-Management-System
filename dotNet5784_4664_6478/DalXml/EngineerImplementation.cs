namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    const string engineerFile = @"..\xml\engineers.xml";
    XDocument dependencyiesDocument = XDocument.Load(engineerFile);
    public int Create(Engineer item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        TextReader textReader = new StringReader(tasksFile);
        List<Engineer lst = (List<Engineer>?)serializer.Deserialize(textReader) ?? throw new Exception();

        //int newDependencyId = Config.NextDependencyId;
        //XElement? dependencyElement = new XElement("Dependency",
        //new XElement("Id", newDependencyId),
        //new XElement("DependentTask", item.DependentTask),
        //new XElement("DependsOnTask", item.DependsOnTask));
        //dependencyiesDocument.Root?.Add(dependencyElement);
        //dependencyiesDocument.Save(dependenciesFile);
        //return newDependencyId;



        //if (Read(item.Id) is not null)
        //    throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        //DataSource.Engineers.Add(item);
        //return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
