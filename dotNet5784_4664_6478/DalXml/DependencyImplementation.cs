namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string dependencysFile = @"..\xml\dependencys.xml";
    XDocument dependencysDocument = XDocument.Load(dependencysFile);

    public int Create(Dependency item)
    {
        int newDependencyId = Config.NextDependencyId;
        XElement? dependencyElement = 
        new XElement("Dependency",
        new XElement("Id", newDependencyId),
        new XElement("DependentTask", item.DependentTask),
        new XElement("DependsOnTask", item.DependsOnTask));
        dependencysDocument.Root?.Add(dependencyElement);
        dependencysDocument.Save(dependencysFile);
        return newDependencyId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
