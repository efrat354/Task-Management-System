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
        XElement? dependencyElement = new XElement("Dependency",
        new XElement("Id", newDependencyId),
        new XElement("DependentTask", item.DependentTask),
        new XElement("DependsOnTask", item.DependsOnTask));
        dependencysDocument.Root?.Add(dependencyElement);
        dependencysDocument.Save(dependencysFile);
        return newDependencyId;
    }

    public void Delete(int id)
    {
        if (dependencysDocument.Root!=null)
        {
            XElement? dependencyElement= dependencysDocument.Root.Elements("Dependency")
                .FirstOrDefault(d => (int)d.Element("Id")! == id);
            if (dependencyElement!=null)
            {
                dependencyElement.Remove();
                dependencysDocument.Save(dependencysFile);
            }
            else 
            {
                throw new DalDoesNotExistException($"The dependency with Id={id} does not exist in the system");
            }
        }
        else
        {
            throw new DalDoesNotExistException("The dependencies document is empty");
        }
    }

    public Dependency? Read(int id)
    {
        if (dependencysDocument.Root != null)
        {
            XElement? dependencyElement = dependencysDocument.Root.Elements("Dependency")
                .FirstOrDefault(d => (int)d.Element("Id")! == id);

            //return XMLTools.ToEnumNullable<Dependency>(dependencyElement, " ");
        }
        else
        {
            throw new DalDoesNotExistException("The dependencies document is empty");
        }
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        if (dependencysDocument.Root != null)
        {
            XElement? dependencyElement = dependencysDocument.Root.Elements("Dependency")
                .FirstOrDefault(filter);

            //return XMLTools.ToEnumNullable<Dependency>(dependencyElement, " ");
        }
        else
        {
            throw new DalDoesNotExistException("The dependencies document is empty");
        }
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
