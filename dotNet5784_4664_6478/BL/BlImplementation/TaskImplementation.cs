﻿

using BlApi;
using BO;
using System.Collections.Generic;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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
    private string Validation(BO.Task boTask)
    {
        if (boTask.Id <= 0)
        {
            return "Id is not valid";
        }
        if (boTask.Alias != "")
        {
            return "Alias is not valid";
        }
        else
        {
            return "";
        }
    }
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

    public void Create(BO.Task boTask)
    {
        bool isMilestone = false;
        string message = Validation(boTask);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        if (boTask.Dependencies != null)
        {
            isMilestone = true;
            var listDep = from BO.TaskInList dependency in boTask.Dependencies!
                          select new DO.Dependency
                          {
                              DependentTask = boTask.Id,
                              DependsOnTask = dependency.Id
                          };
            listDep.Select(dep => _dal.Dependency.Create(dep));
        }
        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boTask.DeadlineDate - boTask.StartDate));
        DO.Task doTask = new DO.Task
               (0, boTask.Alias, boTask.Description, boTask.CreatedAtDate, requiredEffortTime,
               isMilestone, (DO.EngineerExperience)boTask.ComplexityLevel,
               boTask.StartDate, boTask.ForecastDate, boTask.DeadlineDate,
               boTask.CompleteDate, boTask.Product, boTask.Remarks, boTask.Engineer?.Id);
        _dal.Task.Create(doTask);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    /* public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        }

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Active = doEngineer.Active,
            Task = FindTask(id)
        };
    }*/
    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
       
        //מציאת הנדס של המשימה
    //  DO.Engineer engineer=_dal.Engineer.Read((int?)doTask.EngineerId);
        return new BO.Task()
        {
            Id = id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = CreateStatus(doTask),
            Dependencies = (List<TaskInList>)listDep,
          //  Milestone = listDep==null? false :true,
          // ScheduledStartDate = doTask.ScheduledDate,//?
            StartDate = doTask.StartDate,
            ForecastDate = doTask.ScheduledDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
              // Engineer = doTask.EngineerId
            // Complexity =(BO.EngineerExperience) doTask.Complexity,
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
    //    public int Id { get; init; }
    //public required string Alias { get; set; }
    //public required string Description { get; set; }
    //public required DateTime CreatedAtDate { get; init; }
    //public Status? Status { get; set; }
    //public List<TaskInList>? Dependencies { get; set; }
    //public Milestone? Milestone { get; set; }
    //public DateTime? ScheduledStartDate { get; set; }//הוא כתב baselinedate מה צריך לעשות עם התאריך הזה
    //public DateTime? StartDate { get; set; }
    //public DateTime? ForecastDate { get; set; }//ScheduledEndDate
    //public DateTime? DeadlineDate { get; set; }
    //public DateTime? CompleteDate { get; set; }
    //public string? Product { get; set; }
    //public string? Remarks { get; set; }
    //public EngineerInTask? Engineer { get; set; }//????
    //public EngineerExperience ComplexityLevel { get; set; }
        return from DO.Task doTask in _dal.Task.ReadAll(filter)
               let engineerId=doTask.EngineerId
               select new BO.Task()
               {
                   Id = doTask.Id,
                   Alias=doTask.Alias,
                   Description = doTask.Description,
                   CreatedAtDate=doTask.CreatedAtDate,
                   Status = CreateStatus(doTask),
                   //Dependencies = (List<TaskInList>),
                   //milestone
                   ScheduledStartDate=doTask.ScheduledDate,//????
                   StartDate = doTask.StartDate,
                   ForecastDate=doTask.ScheduledDate,
                   DeadlineDate=doTask.DeadlineDate,
                   CompleteDate=doTask.CompleteDate,
                   Product = doTask.Product,
                   Remarks=doTask.Remarks,
                   Engineer= doTask.EngineerId == null ? null: new EngineerInTask() 
                   { Id= (int)doTask.EngineerId, Name=_dal.Engineer.Read((int)doTask.EngineerId)!.Name },
                   ComplexityLevel= (BO.EngineerExperience)doTask.Complexity
                   //??Active
               };
    }

    public void Update(BO.Task task)
    {
        throw new NotImplementedException();
    }
}
