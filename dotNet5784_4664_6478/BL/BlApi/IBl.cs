namespace BlApi;
/// <summary>
///IBl interface
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITask Task { get; }
    public IEngineerInTask EngineerInTask { get; }
    public IMilestoneInList MilestoneInList { get; }
}
