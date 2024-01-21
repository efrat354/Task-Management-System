namespace BlImplementation;
using BlApi;
using BO;
using DO;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.Status CreateStatus(DO.Task doTask)
    {
        Status status = Status.Unscheduled;
        if (doTask.ScheduledDate != null && doTask.StartDate == null)
            status = Status.Scheduled;
        else if (doTask.StartDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.OnTrack;
        else if (doTask.DeadlineDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.InJeopardy;
        else if (doTask.CompleteDate != null && doTask.CompleteDate < DateTime.Now)
            status = Status.InJeopardy;
        return status;
    }
    private string Validation(BO.Task boTask)
    {
        if (boTask.Alias == "")
        {
            return "Alias is not valid";
        }
        else
        {
            return "";
        }
    }
    private List<BO.TaskInList> FindDependencies(int id)
    {
        /* IEnumerable<BO.TaskInList>*/
        var listDep = from DO.Dependency dependency in _dal.Dependency.ReadAll()
                      where dependency.DependentTask == id
                      let task = _dal.Task.Read(dependency.DependsOnTask)
                      select new BO.TaskInList
                      {
                          Id = task.Id,
                          Alias = task.Alias,
                          Description = task.Description,
                          Status = CreateStatus(task)
                      };
        return listDep.ToList();


        // _dal.Dependency.ReadAll().Select(t=> {
        // if (t.DependentTask == id)
        // {
        //     return t;
        // }
        // else
        //     return null;
        //});
    }
    private BO.MilestoneInTask FindMilestone(int id)
    {
        IEnumerable<BO.MilestoneInTask> milstone = from DO.Task task in _dal.Task.ReadAll()
                                                   where task.IsMilestone == true
                                                   where FindDependencies(task.Id)
                                                   .FirstOrDefault(dep => dep.Id == id) != null
                                                   let t = _dal.Task.Read(task.Id)
                                                   select new BO.MilestoneInTask()
                                                   {
                                                       Id = t.Id,
                                                       Alias = t.Alias
                                                   };
        //BO.Milestone mil=new BO.Milestone() { Id= milstone.Id,Alias=milstone }
        return (BO.MilestoneInTask)milstone;
    }

    public void Create(BO.Task boTask)
    {
        bool isMilestone = false;
        string message = Validation(boTask);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        if (_dal.Engineer.Read(boTask.Engineer!.Id) == null)
        {
            throw new BlDoesNotExistException("Can not create the task, engineer is not exist");
        }

        TimeSpan requiredEffortTime = new TimeSpan(Convert.ToInt32(boTask.DeadlineDate - boTask.StartDate));//לבדוק מה לשים את הtimespan שהתקבל או את החישוב
        DO.Task doTask = new DO.Task
             (0, boTask.Alias, boTask.Description, boTask.CreatedAtDate, requiredEffortTime,
             false, (DO.EngineerExperience)boTask.ComplexityLevel,//במקום שהיה פה isMilstone שמתי false
             boTask.StartDate, boTask.ScheduledStartDate, boTask.DeadlineDate,
             boTask.CompleteDate, boTask.Product, boTask.Remarks, boTask.Engineer?.Id);
        int depenId = _dal.Task.Create(doTask);

        if (boTask.Dependencies != null)
        {
            //isMilestone = true;
            var listDep = from BO.TaskInList dependency in boTask.Dependencies!
                          select new DO.Dependency
                          {
                              DependentTask = depenId,
                              DependsOnTask = dependency.Id
                          };
            foreach (var dep in listDep)
            {
                _dal.Dependency.Create(dep);
            }
            //listDep.Select(dep => _dal.Dependency.Create(dep));
        }
    }

    public void Delete(int id)
    {
        BO.Task task = Read(id);
        if (task != null)
        {
            var isPrevious = _dal.Dependency.ReadAll()
                .FirstOrDefault(dep => dep?.DependsOnTask == id);
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
            Dependencies = FindDependencies(doTask.Id),
            Milestone = null,//FindMilestone(doTask.Id),
            ScheduledStartDate = doTask.ScheduledDate,
            StartDate = doTask.StartDate,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
            Engineer = doTask.EngineerId == null ? null : new EngineerInTask()
            { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name },
            ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task?, bool>? filter = null)//תאריכים לא מסונכרנים,MILSTONE,האם צריך ? בכותרת
    {
        return from DO.Task doTask in _dal.Task.ReadAll(filter)
               select new BO.Task()
               {
                   Id = doTask.Id,
                   Alias = doTask.Alias,
                   Description = doTask.Description,
                   CreatedAtDate = doTask.CreatedAtDate,
                   Status = CreateStatus(doTask),
                   Dependencies = FindDependencies(doTask.Id),//אם יש MILSTONE  אז יש גם DEPENDENCIES
                   Milestone = null,//new BO.MilestoneInTask { Id = FindMilestone(doTask.Id).Id, Alias = FindMilestone(doTask.Id).Alias },//FindMilestone(doTask.Id), // new BO.MilestoneInTask() { Id=0,Alias="n"},//FindMilestone(doTask.Id),
                   ScheduledStartDate = doTask.ScheduledDate,
                   StartDate = doTask.StartDate,
                   DeadlineDate = doTask.DeadlineDate,
                   CompleteDate = doTask.CompleteDate,
                   Product = doTask.Product,
                   Remarks = doTask.Remarks,
                   Engineer = doTask.EngineerId == null ? null : new EngineerInTask()
                   { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name },
                   ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
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
              boTask.Dependencies == null ? false : true, (DO.EngineerExperience)boTask.ComplexityLevel,
               boTask.StartDate, boTask.ScheduledStartDate, boTask.DeadlineDate,
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
