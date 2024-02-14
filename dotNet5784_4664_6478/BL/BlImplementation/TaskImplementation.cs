namespace BlImplementation;
using BlApi;
using BO;

internal class TaskImplementation : ITask
{
    /// <summary>
    ///  A class that implements task
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// The function Calculates the appropriate status for the task
    /// </summary>
    /// <param name="doTask">The task we want to calculate a status from her task</param>
    /// <returns>Matching status</returns>
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
        else status = Status.Done;
        return status;
    }
    /// <summary>
    /// A function that checks whether the Alias is correct
    /// </summary>
    /// <param name="boTask">The task its alias should be checked</param>
    /// <returns>if correct true otherwise false</returns>
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
    /// <summary>
    /// A function that finds which tasks depend on a particular task
    /// </summary>
    /// <param name="id">ID number of the task for which dependencies are searched</param>
    /// <returns>the list of Dependency tasks </returns>
    private List<BO.TaskInList> FindDependencies(int id)
    {
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
    }
    /// <summary>
    /// A function that finds the milestone of the sent message
    /// </summary>
    /// <param name="id">id of the task for which we are looking for a milestone</param>
    /// <returns>The appropriate Milestone</returns>
    private BO.MilestoneInTask FindMilestone(int id)
    {
        int idMil = (from DO.Task task in _dal.Task.ReadAll()
                   where task.IsMilestone == true
                   where FindDependencies(task.Id)
                   .FirstOrDefault(dep => dep.Id == id) != null
                   select task.Id).FirstOrDefault();

        DO.Task tk = _dal.Task.Read(idMil)!;
        BO.MilestoneInTask? milestone = tk != null?new BO.MilestoneInTask() { Id = tk.Id, Alias = tk.Alias }:null;
        return milestone!;
    }
    /// <summary>
    /// The function adds an task to the data
    /// </summary>
    /// <param name="boTask">The new task</param>
    /// <returns>The new task id</returns>
    /// <exception cref="BO.BlInvalidInput">Throw away if the details are incorrect</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Throw if the task already exists</exception>
    public void Create(BO.Task boTask)
    {
        bool isMilestone = false;
        string message = Validation(boTask);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        if (boTask.Engineer?.Id==0||boTask.Engineer?.Name == " ")
        {
            throw new BlDoesNotExistException("Can not create the task, engineer is not exist");
        }
        try
        {
            _dal.Engineer.Read(boTask.Engineer.Id);
        }
        catch (Exception ex)
        {
            throw new BlDoesNotExistException("Can not create the task, engineer is not exist", ex);
        }
        if (boTask.Dependencies != null)
        {
            isMilestone = true;
        }

        DO.Task doTask = new DO.Task
             (0, boTask.Alias, boTask.Description, boTask.CreatedAtDate, boTask.RequiredEffortTime,
             isMilestone, (DO.EngineerExperience)boTask.ComplexityLevel,
             boTask.StartDate, boTask.ScheduledStartDate, boTask.DeadlineDate,
             boTask.CompleteDate, boTask.Product, boTask.Remarks, boTask.Engineer?.Id);
        int depenId = _dal.Task.Create(doTask);

        if (boTask.Dependencies != null)
        {
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
        }
    }
    /// <summary>
    /// An action that deletes an task from the data
    /// </summary>
    /// <param name="id">The engineer you want to remove</param>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the object you want to remove does not exist</exception>
    /// <exception cref="BO.BlDeletionImpossible">Throw if the task cannot be deleted</exception>
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
    /// <summary>
    /// A function that calls a certain object from the data
    /// </summary>
    /// <param name="id">The task's id you want to call</param>
    /// <returns>the desired task</returns>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the task does not exist in the data</exception>
    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        }
        List<BO.TaskInList> dependencies = FindDependencies(doTask.Id);
        return new BO.Task()
        {
            Id = doTask.Id,
            Alias = doTask.Alias,
            Description = doTask.Description,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = CreateStatus(doTask),
            Dependencies = dependencies,
            Milestone = doTask.IsMilestone || dependencies.Count() == 0 ? null :FindMilestone(doTask.Id),
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
    private string FindName(int id)
    {
         DO.Engineer? eng= _dal.Engineer.Read(id);
        string name = eng != null ? eng.Name : "";
        return name;
    }
    /// <summary>
    /// A function that reads all existing tasks either with or without conditions
    /// </summary>
    /// <param name="filter">A parameter by which you can filter which tasks you want</param>
    /// <returns>the desired list of tasks</returns>
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task?, bool>? filter = null)
    {
        return from DO.Task doTask in _dal.Task.ReadAll(filter)
               let dependencies = FindDependencies(doTask.Id)
               select new BO.Task()
               {
                   Id = doTask.Id,
                   Alias = doTask.Alias,
                   Description = doTask.Description,
                   CreatedAtDate = doTask.CreatedAtDate,
                   Status = CreateStatus(doTask),
                   Dependencies = dependencies,
                   Milestone = doTask.IsMilestone || dependencies.Count() == 0 ? null : FindMilestone(doTask.Id),
                   ScheduledStartDate = doTask.ScheduledDate,
                   StartDate = doTask.StartDate,
                   DeadlineDate = doTask.DeadlineDate,
                   CompleteDate = doTask.CompleteDate,
                   Product = doTask.Product,
                   Remarks = doTask.Remarks,
                   Engineer = doTask.EngineerId == null ? null : new EngineerInTask()
                   { Id = (int)doTask.EngineerId, Name = FindName((int)doTask.EngineerId) },
                   ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
               };
    }
    /// <summary>
    /// A function that updates a particular object with details
    /// </summary>
    /// <param name="boEngineer">Receives an engineer's object with updated details</param>
    /// <exception cref="BO.BlInvalidInput">Throw away if the details are incorrect</exception>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the task does not exist in the data</exception>
    public void Update(BO.Task boTask)
    {
        string message = Validation(boTask);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }

        DO.Task doTask = new DO.Task
               (boTask.Id, boTask.Alias, boTask.Description, boTask.CreatedAtDate, boTask.RequiredEffortTime,
              boTask.Dependencies == null ? false : true, (DO.EngineerExperience)boTask.ComplexityLevel,
               boTask.StartDate, boTask.ScheduledStartDate, boTask.DeadlineDate,
               boTask.CompleteDate, boTask.Product, boTask.Remarks, boTask.Engineer?.Id);
        try
        {
            _dal.Task.Update(doTask);

            foreach (DO.Dependency? dep in _dal.Dependency.ReadAll(dep => dep!.DependentTask == boTask.Id))
                if (dep.Id!=null)
                {
                    _dal.Dependency.Delete(dep!.Id);
                }

            if (boTask.Dependencies != null)
            {
                var listDep = from BO.TaskInList dependency in boTask.Dependencies!
                              select new DO.Dependency
                              {
                                  DependentTask = boTask.Id,
                                  DependsOnTask = dependency.Id
                              };
                foreach (var dep in listDep)
                {
                    _dal.Dependency.Create(dep);
                }
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} does not exists", ex);
        }
    }
}
