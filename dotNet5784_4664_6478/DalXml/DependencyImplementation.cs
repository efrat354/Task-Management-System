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
       //f (dependencysDocument.Root != null)
       
            XElement? dependencyElement = dependencysDocument.Root?
            .Elements("Dependency").FirstOrDefault(d => (int)d.Element("Id")! == id);

         if(dependencyElement!=null)
            {
                Dependency? dependency = new Dependency
                    ((int)dependencyElement.Element("Id")!,
                    (int)dependencyElement.Element("DependentTask")!,
                    (int)dependencyElement.Element("DependsOnTask")!);
                return dependency;
            }

 
        else
        {
            throw new DalDoesNotExistException("The dependencies document is empty");
        }
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? dependencyElements =
         dependencysDocument.Root?.Elements("Dependency")?.
        Select(d => new Dependency(
                (int)d.Element("Id")!,
                (int)d.Element("DependentTask")!,
                (int)d.Element("DependsOnTask")!))
        .FirstOrDefault(filter);
         //if(dependencyElement!=null)
            return dependencyElements;

    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null)
    {
        XElement? dependenciesElement = XMLTools.LoadListFromXMLElement("dependencys");
        

            IEnumerable<Dependency> dependencies = dependenciesElement
         .Elements("Dependency")
         .Select(e => new Dependency(
              (int)e.Element("Id")!,
             (int)e.Element("DependentTask")!,
             (int)e.Element("DependsOnTask")!
         ));
        if(dependencies != null)
        {
            dependencies = dependencies.Where(filter);
        }
        return dependencies;

     
    }
    public void Update(Dependency item)
    {

    }
}
