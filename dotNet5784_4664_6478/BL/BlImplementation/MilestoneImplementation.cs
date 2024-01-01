

using BlApi;
using BO;
using System.Collections.Generic;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /*
     var groupedDependencies = _dal.Dependency.ReadAll()
     .OrderBy(dep => dep?.DependsOnTask)
     .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
     .ToList();
     
     */
    public void CreateSchedule()
    {
        BO.Milestone milestone;
        //רשימת אוביקטים מקובצים לפי dependent
        var dependenciesList = _dal.Dependency.ReadAll()
            .OrderBy(dep => dep?.DependsOnTask)//מיון לפי המשימה שתלוים בה
            .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
            .ToList();

        var distinctDependencies = dependenciesList.Distinct().ToList();//מחיקת כפילויות
      //  var milestoneList = from dep in distinctDependencies
                         //   select new BO.Milestone() { }//
    }

    public Milestone Read(int id)
    {
        throw new NotImplementedException();
    }

    public Milestone Update(int id)
    {
        throw new NotImplementedException();
    }
}
