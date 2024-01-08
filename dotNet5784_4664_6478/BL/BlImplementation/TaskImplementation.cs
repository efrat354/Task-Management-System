

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

    public void Delete(int id)//לבדוק האם ? !
    {
        BO.Task task = Read(id);
        if (task != null)
        {
            var isPrevious = _dal.Dependency.ReadAll()
                .FirstOrDefault( dep => dep?.DependsOnTask == id);//???
            if (isPrevious != null)
            {
                throw new BO.BlDeletionImpossible($"Task with id={id} can not be deleted");
            }
            _dal.Task.Delete(id);
        }
        else
        {
            throw new BO.BlDoesNotExistException($"Task with id={id} does not exist");
        }
    }

    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
        return new BO.Task()
        {
            Id = doTask.Id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = CreateStatus(doTask),
            Dependencies = (List<TaskInList>)FindDependencies(doTask.Id),//????
            //milestone
            ScheduledStartDate = doTask.ScheduledDate,//????
            StartDate = doTask.StartDate,
            ForecastDate = doTask.ScheduledDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
            Engineer = doTask.EngineerId == null ? null : new EngineerInTask()
            { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name },
            ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
            //??Active
        };
    }//מה לעשות לגבי DEPENDENCIES, MILESTONE, תאריכים שגויים,כפילות קוד 

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task?, bool>? filter = null)//תאריכים לא מסונכרנים, ACTIVE?,MILSTONE,האם צריך ? בכותרת
    {
        return from DO.Task doTask in _dal.Task.ReadAll(filter)
               select new BO.Task()
               {

                   Id = doTask.Id,
                   Alias = doTask.Alias,
                   Description = doTask.Description,
                   CreatedAtDate = doTask.CreatedAtDate,
                   Status = CreateStatus(doTask),
                   Dependencies = (List<TaskInList>)FindDependencies(doTask.Id),
                   //milestone
                   ScheduledStartDate = doTask.ScheduledDate,//????
                   StartDate = doTask.StartDate,
                   ForecastDate = doTask.ScheduledDate,
                   DeadlineDate = doTask.DeadlineDate,
                   CompleteDate = doTask.CompleteDate,
                   Product = doTask.Product,
                   Remarks = doTask.Remarks,
                   Engineer = doTask.EngineerId == null ? null : new EngineerInTask()
                   { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name },
                   ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
                   //??Active
               };
    }

    public void Update(BO.Task boTask)//האם ניתן לשנות את הDEPENDENCIES ע"י עדכון משימה
    {
        string message = Validation(boTask);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boTask.DeadlineDate - boTask.StartDate));
        DO.Task doTask = new DO.Task
               (boTask.Id, boTask.Alias, boTask.Description, boTask.CreatedAtDate, requiredEffortTime,
              boTask.Dependencies==null?false:true, (DO.EngineerExperience)boTask.ComplexityLevel,
               boTask.StartDate, boTask.ForecastDate, boTask.DeadlineDate,
               boTask.CompleteDate, boTask.Product, boTask.Remarks, boTask.Engineer?.Id);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} does not exists", ex);
        }
    }
}
