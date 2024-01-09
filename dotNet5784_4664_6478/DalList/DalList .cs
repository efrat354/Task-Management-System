namespace Dal;
using DalApi;
using System;

/// <summary>
/// Class that use IDal by creating 3 implementations each one for an one entity 
///And creation dates for the begining and the end of the project. In addition it implements the reset function
///And reset function
/// </summary>

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public DateTime? startDateProject { get => DataSource.Config.startProjectDate; set => DataSource.Config.startProjectDate = value; }
    public DateTime? endDateProject { get => DataSource.Config.endProjectDate; set => DataSource.Config.endProjectDate = value; }

    //Delete all the data
    public void Reset()
    {
        Task.Reset();
        Engineer.Reset();
        Dependency.Reset();
    }
}
