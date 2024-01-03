

using BlApi;
using BO;

namespace BlImplementation;



internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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
    /*
     var groupedDependencies = _dal.Dependency.ReadAll()
     .OrderBy(dep => dep?.DependsOnTask)
     .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
     .ToList();
     
     */
    public void CreateSchedule()
    {
        //יצרנו קבוצה של משימה תלויות וערכים של משימות שבהן היא תלויה ואז עבור כל קבוצה יצרנו אבן דרך שהתלויות שלה זה DEPENDENCIES
        var groups = _dal.Dependency.ReadAll()
    .OrderBy(dep => dep?.DependsOnTask)
    .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
    .Distinct();
        _dal.Dependency.Reset();

        int indexMilestone = 0;//לבדוק מה קורה כשאין תלויות
        var newList = groups.Select(groupDep =>
                      {
                          int idMilstone = _dal.Task.Create(new DO.Task(indexMilestone++, $"milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, new TimeSpan(0), true, 0, null, null, null, null, "", "", null));
                          int idDepNext=_dal.Dependency.Create(new DO.Dependency( (int)groupDep.TaskId!, idMilstone));//יצירת התלות בין משימה 3 לאבן דרך בתרשים
                          return groupDep.Dependencies.Select(dep =>
                          {
                              int idDep = _dal.Dependency.Create(new DO.Dependency(idMilstone, dep!.Value));//יצירת תלויות בין משימות קודמות לאבן דרך
                              return idDep;
                          });
                      });
        int idFirst = _dal.Task.Create(new DO.Task(0, "milestone0", "M0", DateTime.Now, new TimeSpan(0), true, 0, null, null, null, null, "", "", null));
        //פה צריך לעשות תלויות לראשונות
        //חלק 4 ו5 של חישוב אבני דרך

        //BO.Milestone milestone;
        ////רשימת אוביקטים מקובצים לפי dependent
        //var dependenciesList = _dal.Dependency.ReadAll()
        //    .OrderBy(dep => dep?.DependsOnTask)//מיון לפי המשימה שתלוים בה
        //    .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
        //    .ToList();

        //var distinctDependencies = dependenciesList
        //.SelectMany(depGroup => depGroup.Dependencies)
        //.Where(dep => dep != null)
        //.Distinct()
        //.ToList();

        //var newList = from groupDep in groups
        //              select groupDep =>
        //var newList = from groupDep in groups
        //              select groupDep =>
        //              {
        //                  int id = _dal.Task.Create(new DO.Task(indexMilestone++, $"milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, new TimeSpan(0), true, 0, null, null, null, null, "", "", null));


        //              };
        //var newMilstones = from milestone in groups
        //                   select new BO.Milestone()
        //                   {
        //                       Id = (int)milestone.TaskId!,
        //                       Alias = "M",
        //                       Description = "",
        //                       CreatedAtDate = DateTime.Now,
        //                       Status = 0,
        //                       StartDate = DateTime.Now,
        //                       ForecastDate = DateTime.Now,
        //                       DeadlineDate = DateTime.Now,
        //                       CompleteDate = DateTime.Now,
        //                       CompletionPercentage = 0,
        //                       Remarks = " ",
        //                       Dependencies = milestone.Dependencies?.Select(dep => new TaskInList
        //                       {
        //                           Id = dep ?? 0,
        //                           Alias = "",
        //                           Description = "",
        //                           Status = 0,
        //                       }).ToList() ?? new List<TaskInList>() // אם התלות היא null, יצירת רשימה ריקה
        //                   };

        // var newList = groups.

        //select new DO.Dependency()
        //{
        //    Id = dependency.,
        //    DependentTask = dependency.Dependencies

        //}
        //Select(dep=> _dal.Dependency.Create())


    }

    //.Select(depGroup => new BO.Milestone()
    //{
    //    Id = (int)depGroup.TaskId!,
    //    Alias = "M",
    //    Description = "",
    //    CreatedAtDate = DateTime.Now,
    //    Status = 0,
    //    StartDate = DateTime.Now,
    //    ForecastDate = DateTime.Now,
    //    DeadlineDate = DateTime.Now,
    //    CompleteDate = DateTime.Now,
    //    CompletionPercentage = 0,
    //    Remarks = " ",
    //    Dependencies = depGroup.Dependencies?.Select(dep => new TaskInList
    //    {
    //        Id = dep ?? 0,
    //        Alias = "",
    //        Description = "",
    //        Status = 0,
    //    }).ToList() ?? new List<TaskInList>() // אם התלות היא null, יצירת רשימה ריקה
    //}
    //)
    //.ToList();

    //}

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


