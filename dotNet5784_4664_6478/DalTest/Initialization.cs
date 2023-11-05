namespace DalTest;
using DalApi;
using DO;
public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();
    private static void createEngineer()
    { }
    private static void createDependency()
    { }
    private static void createTask()
    { }
 
    public static void Do(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
       
    }

}
