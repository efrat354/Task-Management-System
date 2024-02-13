namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Linq;

/// <summary>
/// Class that implement the IMilestone interface.
///The class includes function that take care of the project's schedule- all the care of milestones.
/// </summary>
internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// The function find the milestone's dependencies
    /// </summary>
    /// <param name="id">The id of the milestone for checking</param>
    /// <returns>List of the milestone's dependencies from type BO.TaskInList</returns>
    private List<BO.TaskInList> FindDependencies(int id)
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
        return listDep.ToList();
    }
    /// <summary>
    /// Function that determine the milestone's status according to its dates from an enum of status
    /// </summary>
    /// <param name="doTask">The task that the function need to find its status</param>
    /// <returns>The milestone's status from type BO.Status </returns>
    private BO.Status CreateStatus(DO.Task doTask)
    {
        Status status = Status.Unscheduled;
        if (doTask.ScheduledDate != null && doTask.StartDate == null)//> DateTime.Now
            status = Status.Scheduled;
        if (doTask.StartDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.OnTrack;
        if (doTask.DeadlineDate < DateTime.Now && doTask.CompleteDate == null)
            status = Status.InJeopardy;
        else if (doTask.CompleteDate != null && doTask.CompleteDate < DateTime.Now)
            status = Status.InJeopardy;
        else status = Status.Done;
        return status;
    }
    /// <summary>
    /// Function that check if the milestone's details are validate
    /// </summary>
    /// <param name="boMilestone">The milestone for checking</param>
    /// <returns>The error message from type string if there isn't error return empty string </returns>
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
    /// <summary>
    /// Function that determine recursively the deadline dates of the project's tasks 
    /// </summary>
    /// <param name="milestone">The current milestone for updating its and its dependencies deadline dates</param>
    /// <param name="DeadLine">The current deadline date according to the current milestone</param>
    private void UpdateDeadlineDates(DO.Task milestone, DateTime DeadLine)
    {
        //Update the deadline date of the milestone
        if (milestone.DeadlineDate == null || milestone.DeadlineDate > DeadLine)
            _dal.Task.Update(new DO.Task(
                   milestone.Id,
                   milestone.Alias,
                   milestone.Description,
                   milestone.CreatedAtDate,
                   milestone.RequiredEffortTime,
                   milestone.IsMilestone,
                   milestone.Complexity,
                   milestone.StartDate,
                   milestone.ScheduledDate,
                   DeadLine, //update the deadline date
                   milestone.CompleteDate,
                   milestone.Product,
                   milestone.Remarks,
                   milestone.EngineerId));
        //Stop condition - if we finish to update all the tasks
        if (milestone.Alias == "Start")
        {
            return;
        }

        //Updating the tasks deadline dates

        Dictionary<int, DateTime> milestones = new Dictionary<int, DateTime>();
        int milestoneId;
        DateTime milestoneDedLine;
        //Going over all the tasks that the milestone dependent on them 
        foreach (DO.Task? task in _dal.Task.ReadAll(t => _dal.Dependency.ReadAll().Any(d => d?.DependentTask == milestone.Id && d.DependsOnTask == t?.Id)))
        {
            //If the task's deadline date is not update yet or its need to update again- its deadline date is bigger then the current deadline
            if (task?.DeadlineDate == null || task.DeadlineDate > DeadLine)
            {
                _dal.Task.Update(new DO.Task(
                               task!.Id,
                               task.Alias,
                               task.Description,
                               task.CreatedAtDate,
                               task.RequiredEffortTime,
                               task.IsMilestone,
                               task.Complexity,
                               task.StartDate,
                               DeadLine.Subtract(task.RequiredEffortTime!),//עדכון תאריך התחלה
                               DeadLine,
                               task.CompleteDate,
                               task.Product,
                               task.Remarks,
                               task.EngineerId
                           ));
                milestoneId = _dal.Dependency.Read(d => d.DependentTask == task.Id)!.DependsOnTask;
                milestoneDedLine = DeadLine.Subtract(task.RequiredEffortTime!);
                if (milestones.ContainsKey(milestoneId))
                {
                    if (milestones[milestoneId] > milestoneDedLine)
                        milestones[milestoneId] = milestoneDedLine;
                }
                else
                    milestones.Add(milestoneId, milestoneDedLine);
            }
        }
        //Update the milestone's tasks
        foreach (var item in milestones)
        {
            UpdateDeadlineDates(_dal.Task.Read(item.Key)!, item.Value);
        }
    }
    public void CreateSchedule()
    {
        int milestoneId;
        List<DO.Dependency> newDependencies = new List<DO.Dependency>();

        //Grouping into groups according to the dependent task
        var groupsByDependentTask = _dal.Dependency.ReadAll()
            .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask,
            (id, dependency) => new
            {
                TaskId = id,
                Dependencies = dependency.OrderBy(dep => dep)
            });

        //Finding all the tasks that do not depend on anyone and these will depend on the first stone
        var allTaskIds = groupsByDependentTask.Select(group => group.TaskId).ToList();
        var taskIdsWithoutDependencies = _dal.Task.ReadAll().Select(task => task?.Id).Except(allTaskIds);

        //All the tasks of the tasks that do not depend on them
        var notDependOnTask = _dal.Task.ReadAll().Select(task => task?.Id).Except(_dal.Dependency.ReadAll().Select(dep => dep?.DependsOnTask));
        notDependOnTask.Select(task => { _dal.Task.Update(_dal.Task.Read((int)task!)! with { DeadlineDate = _dal.endDateProject }); return task; });

        //Adding a milestone for the start of the project
        milestoneId = _dal.Task.Create(new DO.Task()
        {
            Description = $"milestone0",
            Alias = "Start",
            IsMilestone = true,
            CreatedAtDate = DateTime.Now
        });

        //Adding the dependencies between the first stone and tasks that don't depend on the new list
        newDependencies.AddRange(taskIdsWithoutDependencies
        .Select(task => new DO.Dependency(0, (int)task!, milestoneId)));

        //Creating groups according to the DEPENDENCIES in order to remove duplicates, for those groups the group will contain all those that depend on that dependent group
        var groups = groupsByDependentTask
        .GroupBy(dep => dep?.Dependencies, dep => dep?.TaskId,
        (dependencies, taskId) => new
        {
            Dependencies = dependencies,
            TaskIds = taskId
        });

        // For everything else, create dependencies
        int indexMilestone = 1;
        foreach (var group in groups)
        {
            int idMilstone = _dal.Task.Create(new DO.Task
               (indexMilestone++,
               $"milestone{indexMilestone}",
               $"M{indexMilestone}",
               DateTime.Now
               ));
            //Adding dependencies of all the tasks that depend on the new milestone
            newDependencies.AddRange(group.TaskIds.Select(taskId => new DO.Dependency(0, (int)taskId!, idMilstone)));

            //Adding dependencies of the milestone in all tasks preceding it
            newDependencies.AddRange(group.Dependencies!.Select(dep => new DO.Dependency(0, idMilstone, dep!.Value)));
        }

        //Adding a milestone to the end of the project
        milestoneId = _dal.Task.Create(new DO.Task()
        {
            Description = $"milestone{++indexMilestone}",
            Alias = "End",
            IsMilestone = true,
            CreatedAtDate = DateTime.Now
        });

        //Adding the dependencies between the last stone and the group that does not depend on it
        newDependencies.AddRange(notDependOnTask.Select(task => new DO.Dependency(0, milestoneId, (int)task!)));

        //Adding all dependencies to DO
        _dal.Dependency.Reset();
        foreach (DO.Dependency dependency in newDependencies)
        {
            _dal.Dependency.Create(dependency);
        }

        //Update deadline dates of the tasks
        UpdateDeadlineDates(_dal.Task.Read(milestoneId)!, _dal.endDateProject ?? throw new BO.BlNullPropertyException("End date of the project is null"));

        DateTime scheduledDate;
        //Update start dates for milestones
        foreach (DO.Task? milestone in _dal.Task.ReadAll(t => t.IsMilestone))
        {
            if (milestone?.Alias == "Start")
                scheduledDate = _dal.startDateProject ?? throw new BO.BlNullPropertyException("Start date of the project is null");
            else
                scheduledDate = _dal.Task.ReadAll(t => _dal.Dependency.ReadAll().Any(d => d.DependentTask == milestone.Id && d.DependsOnTask == t.Id))
                    .Min(t => t.ScheduledDate!.Value);

            _dal.Task.Update(new DO.Task(
                   milestone!.Id,
                   milestone.Alias,
                   milestone.Description,
                   milestone.CreatedAtDate,
                   milestone.RequiredEffortTime,
                   milestone.IsMilestone,
                   milestone.Complexity,
                   milestone.StartDate,
                   scheduledDate, //update start date
                   milestone.DeadlineDate,
                   milestone.CompleteDate,
                   milestone.Product,
                   milestone.Remarks,
                   milestone.EngineerId
                   ));
        }
    }
    /// <summary>
    /// Function that search for milestone by its id and return its details 
    /// </summary>
    /// <param name="id">The milestone's id for searching</param>
    /// <returns>The found milestone from type Milestone</returns>
    /// <exception cref="BO.BlDoesNotExistException">If the milestone is not exist throw null exception</exception>
    public Milestone Read(int id)
    {
        BO.Milestone? milestone = null;
        DO.Task? doMilestone = _dal.Task.Read(id);//loading a milestone from the data layer
        if (doMilestone != null)
        {
            List<BO.TaskInList>? dependencies = FindDependencies(id);
            milestone = new Milestone()
            {
                Id = doMilestone.Id,
                Alias = doMilestone.Alias,
                Description = doMilestone.Description,
                CreatedAtDate = doMilestone.CreatedAtDate,
                Status = CreateStatus(doMilestone),
                ScheduledStartDate = doMilestone.ScheduledDate,
                StartDate = doMilestone.StartDate,
                DeadlineDate = doMilestone.DeadlineDate,
                CompleteDate = doMilestone.CompleteDate,
                CompletionPercentage = dependencies != null ? (dependencies.Count(dep => dep.Status == Status.Done) / dependencies.Count()) : 0,
                Remarks = doMilestone.Remarks,
                Dependencies = dependencies,
            };
        }
        else
        {
            throw new BO.BlDoesNotExistException($"The milestone with id ={id} does not exist");
        }
        return milestone;
    }
    /// <summary>
    /// Function that update the milestone's details
    /// </summary>
    /// <param name="boMilestone">The milestone for updating</param>
    /// <returns>The updated milestone</returns>
    /// <exception cref="BO.BlInvalidInput">If the details for updating aren't valid</exception>
    /// <exception cref="BO.BlDoesNotExistException">if the milstone for updating isn't exist</exception>
    public Milestone Update(BO.Milestone boMilestone)
    {
        string message = Validation(boMilestone);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }

        DO.Task doMilestone = _dal.Task.Read(boMilestone.Id)! with
        {
            Alias = boMilestone.Alias,
            Description = boMilestone.Description,
            Remarks = boMilestone.Remarks
        };

        try
        {
            _dal.Task.Update(doMilestone);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milstone with ID={doMilestone.Id} does not exists", ex);
        }
        return boMilestone;
    }
}