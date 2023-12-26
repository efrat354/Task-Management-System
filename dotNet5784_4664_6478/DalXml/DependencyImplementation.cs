namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    //Implementation of the dependency interface

    const string dependenciesFile = @"..\xml\dependencies.xml";
    XDocument dependencyiesDocument = XDocument.Load(dependenciesFile);
    //Gets a dependency ,Create a copy of a dependency and add it to the dependencies' xml file
    public int Create(Dependency item)
    {
        int newDependencyId = Config.NextDependencyId;
        XElement? dependencyElement =
        new XElement("Dependency",
        new XElement("Id", newDependencyId),
        new XElement("DependentTask", item.DependentTask),
        new XElement("DependsOnTask", item.DependsOnTask));
        dependencyiesDocument.Root?.Add(dependencyElement);
        dependencyiesDocument.Save(dependenciesFile);
        return newDependencyId;
    }
    //Gets ID and deleting the dependency with the Received ID from the list

    public void Delete(int id)
    {
        if (dependencyiesDocument.Root != null)
        {
            XElement? dependencyElement = dependencyiesDocument.Root.Elements("Dependency")
                .FirstOrDefault(d => (int)d.Element("Id")! == id);
            if (dependencyElement != null)
            {
                dependencyElement.Remove();
                dependencyiesDocument.Save(dependenciesFile);
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
    //Gets ID and check if it exists in the list 

    public Dependency? Read(int id)
    {
        XElement? dependencyElement = dependencyiesDocument.Root?
        .Elements("Dependency").FirstOrDefault(d => (int)d.Element("Id")! == id);
        Dependency? dependency = new Dependency
            ((int)dependencyElement?.Element("Id")!,
            (int)dependencyElement?.Element("DependentTask")!,
            (int)dependencyElement?.Element("DependsOnTask")!);
        return dependency;
    }
    //Gets a pointer to a boolean function which will go through the dependencies' xml file and return the first dependency in the list on which the function returns True.

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dependencyElement = dependencyiesDocument.Root?.Elements("Dependency")?.
        Select(d => new Dependency(
                (int)d.Element("Id")!,
                (int)d.Element("DependentTask")!,
                (int)d.Element("DependsOnTask")!))
        .FirstOrDefault(filter);
        return dependencyElement;
    }
    //Gets a pointer to a boolean function ,which will go through the dependencies' xml file and return the list of all dependencies objects in the list for which the function returns True. If no pointer is sent the entire list will be returned.

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        XElement ?dependenciesElement = XMLTools.LoadListFromXMLElement("dependencyies");
        IEnumerable<Dependency> dependencies = dependenciesElement
         .Elements("Dependency")
         .Select(e => new Dependency(
             (int)e.Element("Id")!,
             (int)e.Element("DependentTask")!,
             (int)e.Element("DependsOnTask")!
         ));
        if (dependencies != null && filter != null)
        {
            dependencies = dependencies.Where(filter);
        }
        return dependencies!;
    }
    //Gets a dependency and update the dependency with the same ID from the dependencies list 

    public void Update(Dependency item)
    {
        if (dependencyiesDocument.Root != null)
        {
            XElement? dependencyElement = dependencyiesDocument.Root.Elements("Dependency")
                .FirstOrDefault(d => (int)d.Element("Id")! == item.Id);
            if (dependencyElement != null)
            {
                dependencyElement.Remove();
                dependencyElement = new XElement("Dependency",
                new XElement("Id", item.Id),
                new XElement("DependentTask", item.DependentTask),
                new XElement("DependsOnTask", item.DependsOnTask));
                dependencyiesDocument.Root?.Add(dependencyElement);
                dependencyiesDocument.Save(dependenciesFile);
            }
            else
            {
                throw new DalDoesNotExistException($"The dependency with Id={item.Id} does not exist in the system");
            }
        }
        else
        {
            throw new DalDoesNotExistException("The dependencies document is empty");
        }
    }

}
