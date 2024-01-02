

using BlApi;
using BO;
using DalApi;
using System.Collections.Generic;

namespace BlImplementation;



internal class MilestoneImplementation : IMilestone
{
    private string Validation(BO.Milestone boMilestone)
    {

        if (boMilestone.Alias != "")
        {
            return "Alias is not valid";
        }
        if (boMilestone.Description != "")
        {
            return "Description is not valid";
        }

        else
        {
            return "";
        }
    }
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

    public Milestone Update(BO.Milestone boMilestone)//מה עושים עם complexity,engineer id,product?
    {
        //BO.Milestone boMilestone=Read(milestone.Id);

        string message = Validation(boMilestone);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boMilestone.DeadlineDate - boMilestone.StartDate));
        DO.Task doMilestone = new DO.Task
               (boMilestone.Id, boMilestone.Alias, boMilestone.Description, boMilestone.CreatedAtDate, requiredEffortTime,
               true, 0,
               boMilestone.StartDate, boMilestone.ForecastDate, boMilestone.DeadlineDate,
               boMilestone.CompleteDate, "", boMilestone.Remarks, null);
        try
        {
            _dal.Task.Update(doMilestone);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={doMilestone.Id} does not exists", ex);
        }
        return boMilestone;
    }

}


