

using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;

namespace BlImplementation;



internal class MilestoneImplementation : IMilestone
{
    private IEnumerable<BO.TaskInList> FindDependencies(int id)
    {
        var listDep = from DO.Dependency dependency in _dal.Dependency.ReadAll()
                      where dependency.DependentTask == id
                      let task = _dal.Task.Read(dependency.DependentTask)
                      select new BO.TaskInList
                      {
                          Id = task.Id,
                          Alias = task.Alias,
                          Description = task.Description,
                          Status = CreateStatus(task)
                      };
        return listDep;
    }
    private BO.Status CreateStatus(DO.Task doTask)
    {
        Status status = Status.Unscheduled;
        if (doTask.ScheduledDate != null && doTask.StartDate == null)//> DateTime.Now
            status = Status.Scheduled;
        if (doTask.StartDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.OnTrack;
        if (doTask.DeadlineDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.InJeopardy;
        //מה עושים כשהוא גמר את המשימה
        return status;
    }
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

    public Milestone Read(int id)//CompletionPercentage ליצור 
    {
        BO.Milestone? milestone = null;
        DO.Task? doMilestone = _dal.Task.Read(id);//טעינת אבן דרך משכבת הנתונים
        if (doMilestone != null)
        {
            milestone = new Milestone()
            {
                Id = doMilestone.Id,
                Alias = doMilestone.Alias,
                Description = doMilestone.Description,
                CreatedAtDate = doMilestone.CreatedAtDate,
                Status = CreateStatus(doMilestone),
                StartDate = doMilestone.StartDate,
                ForecastDate = doMilestone.ScheduledDate,
                DeadlineDate = doMilestone.DeadlineDate,
                CompleteDate = doMilestone.CompleteDate,
                //ניצור פונקציה שבודקת כמה משימות שברשימת התלויות הסתימו
                // CompletionPercentage=doMilestone.
                Remarks = doMilestone.Remarks,
                Dependencies = (List<BO.TaskInList>)FindDependencies(id),

            };
        }
        else
        {
            throw new BO.BlDoesNotExistException($"The milestone with id ={id} does not exist");
        }
        return milestone;
    }

    public Milestone Update(BO.Milestone boMilestone)//האם העתקת ה milestone נכונה
    {
        string message = Validation(boMilestone);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boMilestone.DeadlineDate - boMilestone.StartDate));

        DO.Task doMilestone = _dal.Task.Read(boMilestone.Id)! with { Alias = boMilestone.Alias, Description = boMilestone.Description, Remarks = boMilestone.Remarks };
        //(boMilestone.Id, boMilestone.Alias, boMilestone.Description,
        //boMilestone.CreatedAtDate, requiredEffortTime, true, 0,
        //boMilestone.StartDate, boMilestone.ForecastDate, boMilestone.DeadlineDate,
        //boMilestone.CompleteDate, "", boMilestone.Remarks, null);
        try
        {
            _dal.Task.Update(doMilestone);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={doMilestone.Id} does not exists", ex);
        }
        return boMilestone;
    }

}


