
using DalApi;
using System.Diagnostics;

namespace Dal;
/// <summary>
/// Class that use IDal by creating 3 implementations each one for an one entity 
/// ///And creation dates for th ebegining and the end of the project. In addition it implements the reset function
/// </summary>

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public DateTime? startDateProject { get => Config.startProjectDate; set => Config.startProjectDate = value; }
    public DateTime? endDateProject { get => Config.endProjectDate; set => Config.endProjectDate = value; }

    //Delete all the data
    public void Reset()
    {
        Task.Reset();
        Dependency.Reset();
        Engineer.Reset();
    }
}
