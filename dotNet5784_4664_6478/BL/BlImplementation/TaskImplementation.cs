﻿

using BlApi;
using System.Collections.Generic;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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

    public void Create(BO.Task boTask)
    {
        bool isMilestone =false;
        string message= Validation(boTask);
        if (message!="")
        {
            throw new BO.BlInvalidInput(message);
        }
        if (boTask.Dependencies!=null)
        {
            isMilestone = true;
           var listDep = from BO.TaskInList dependency in boTask.Dependencies!
                          select new DO.Dependency
                          {
                              DependsOnTask = dependency.Id,
                              DependentTask = boTask.Id
                          };
            listDep.Select(dep => _dal.Dependency.Create(dep));
        }
        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boTask.DeadlineDate - boTask.StartDate));
        DO.Task doTask = new DO.Task
               (0, boTask.Alias, boTask.Description, boTask.CreatedAtDate, requiredEffortTime,
               isMilestone,(DO.EngineerExperience)boTask.ComplexityLevel,
               boTask.StartDate,boTask.ForecastDate, boTask.DeadlineDate, 
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
        if(doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
        return new BO.Task()
        {
            Id = id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            //   Status = doTask.,
            //     Dependencies = doTask,
            //   Milestone = doTask
            // ScheduledStartDate = doTask.ScheduledDate,//?
            StartDate = doTask.StartDate,
             ForecastDate = doTask.ScheduledDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
            //   Engineer = doTask.EngineerId
            Complexity =(BO.EngineerExperience) doTask.Complexity,
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task task)
    {
        throw new NotImplementedException();
    }
}
