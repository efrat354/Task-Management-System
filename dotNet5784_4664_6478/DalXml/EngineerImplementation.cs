namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    const string engineerFile = @"..\xml\engineers.xml";
    XDocument dependencyiesDocument = XDocument.Load(engineerFile);
    public int Create(Engineer item)
    {
        throw new NotImplementedException();
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
