
namespace Dal;
internal static class DataSource
{
    //A department that is responsible for a running ID number
    internal static class Config
    {
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
    }
      //Dependencies list
    internal static List<DO.Dependency?> Dependencies { get; } = new();
    //task's list
    internal static List<DO.Task?> Tasks { get; } = new();
    //engineer list
    internal static List<DO.Engineer?> Engineers { get; } = new();


}
