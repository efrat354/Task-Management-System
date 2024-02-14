

namespace BlApi;
/// <summary>
///Milestone interface
/// </summary>
public interface IMilestone
{
    public void CreateSchedule();
   
    public BO.Milestone Read(int id);
   
    public BO.Milestone Update(BO.Milestone milestone);
}
