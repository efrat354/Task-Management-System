namespace DO;
/// <summary>
/// Entity that describe dependency between two tasks
/// </summary>
/// <param name="Id"> An id -continuous number of the dependency</param>
/// <param name="DependentTask"> A task that must be done before the task that depends on it</param>
/// <param name="DependsOnTask">A task that can only be done after the completion of the task it depends on</param>
public record Dependency
(
    int Id,
    int? DependentTask=null,
    int? DependsOnTask = null
)
{
    public Dependency() : this(0) { }
}
