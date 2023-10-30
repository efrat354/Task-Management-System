
namespace Dal;
internal static class DataSource
{
    internal static class Config
    {
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
    }
       

        //איך יודעים שזה הפניה ו nullable
    internal static List<DO.Dependency?> Dependencys { get; } = new();
    internal static List<DO.Task?> Tasks { get; } = new();
    internal static List<DO.Engineer?> Engineers { get; } = new();


}
