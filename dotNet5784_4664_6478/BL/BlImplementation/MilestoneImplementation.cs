﻿
namespace BlImplementation;
using BlApi;
using System.Net.Mail;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.TaskInEngineer? FindTask(int id)
    {
        BO.TaskInEngineer? taskInEngineer;
        DO.Task? task = _dal.Task.ReadAll().FirstOrDefault(t => t?.EngineerId == id);
        if (task == null)
        {
            taskInEngineer = null;
        }
        else
        {
            taskInEngineer = new BO.TaskInEngineer() { Id = task.Id, Alias = task.Alias };
        }
        return taskInEngineer;
    }
    private bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    private string Validation(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0)
        {
            return "Id is not valid";
        }
        if (boEngineer.Name == "")
        {
            return "Name is not valid";
        }
        if (!IsValidEmail(boEngineer.Email))
        {
            return "Email is not valid";
        }
        if (boEngineer.Cost <= 0)
        {
            return "Cost is not valid";
        }
        else
        {
            return "";
        }
    }
   
    public void CreateSchedule()
    {
        //כל הת.ז של המשימות שלא תלויים בהם
        var notDependOnTask = _dal.Task.ReadAll().Select(task => task?.Id).Except(_dal.Dependency.ReadAll().Select(dep => dep?.DependsOnTask));
        notDependOnTask.Select(task => { _dal.Task.Update(_dal.Task.Read((int)task!)! with { DeadlineDate = _dal.endDateProject }); return task; } );


        //פה צריך לעשות רקורסיה שעבור כל משימה נחשב את זמן הסיום האחרון של המשימה הקודמת פחות זמן ביצוע המשימה הנוכחית
        //נתחיל מאלה שלא תלויים בהם, בנוסף נשאל האם זה ריק אז נשנה ואחכ האם התאריך קטן יותר מהתאריך שכבר נמצא
     
        //קיבוץ ע"פ המשימה התלויה
        var groupsByDependentTask = _dal.Dependency.ReadAll()
    .OrderBy(dep => dep?.DependsOnTask)
    .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask,
    (id, dependency) => new { TaskId = id, Dependencies = dependency });
        //מציאת כל הת.ז של המשימות שלא תלויות באף אחד
        var allTaskIds= groupsByDependentTask.Select(group=>group.TaskId).ToList();
        var taskIdsWithoutDependencies = _dal.Task.ReadAll().Select(task => task?.Id).Except(allTaskIds);
        //יצירת קבוצות ע"פ הDEPENDENCIES 
        var groups = groupsByDependentTask
    .GroupBy(dep => dep?.Dependencies, dep => dep?.TaskId,
    (dependencies, taskId) => new { Dependencies = dependencies, TaskIds = taskId });

        _dal.Dependency.Reset();
        //יצירת האבן דרך הראשונה 
        int idFirst = _dal.Task.Create(new DO.Task(0, "milestone0", "M0", DateTime.Now, new TimeSpan(0), true, 0, null, null, null, null, "", "", null));
        //יצרית תלויות עבור ההמישמות שלא תלויות באף אחד לאבן הראשונה 
        taskIdsWithoutDependencies.Select(taskId =>
        {
            throw new BO.BlInvalidInput(message);
        }
        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

                           groupDep.TaskIds.Select(taskId =>
                           {
                               return _dal.Dependency.Create(new DO.Dependency((int)taskId!, idMilstone));
                           });

                           return groupDep.Dependencies?.Select(dep =>
                           {
                               return _dal.Dependency.Create(new DO.Dependency(idMilstone, dep!.Value));//יצירת תלויות בין משימות קודמות לאבן דרך
                           });
                       });
    }


  //  private void ScheduleTasks()
  //  {
  //      var groupsByDependentTask = _dal.Dependency.ReadAll()
  //.OrderBy(dep => dep?.DependsOnTask)
  //.GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask,
  //(id, dependency) => new { TaskId = id, Dependencies = dependency }).ToList();
  //      foreach (var task in groupsByDependentTask)
  //      {
  //          SetEstimatedCompletionDate((int)task.TaskId!,task.Dependencies.ToList());
  //      }
  //  }

  //  private DateTime SetEstimatedCompletionDate(int id,List<int?> dependencies)
  //  {
  //      if (dependencies.Count == 0)
  //      {
  //          _dal.Task.Update(_dal.Task.Read(id)! with { DeadlineDate = _dal.endDateProject });
  //          //task.EstimatedCompletionDate = projectCompletionDate.AddMinutes(-task.Duration);
  //      }
  //      else
  //      {
  //          DateTime maxDependencyCompletionDate = DateTime.MinValue;
  //          foreach (var dependency in task.Dependencies)
  //          {
  //              DateTime dependencyCompletionDate = SetEstimatedCompletionDate(dependency, projectCompletionDate);
  //              if (dependencyCompletionDate > maxDependencyCompletionDate)
  //              {
  //                  maxDependencyCompletionDate = dependencyCompletionDate;
  //              }
  //          }

  //          task.EstimatedCompletionDate = maxDependencyCompletionDate.AddMinutes(-task.Duration);
  //      }

  //      return task.EstimatedCompletionDate;
  //  }



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
                ScheduledStartDate = doMilestone.ScheduledDate,
                StartDate = doMilestone.StartDate,
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

//יצרנו קבוצה של משימה תלויות וערכים של משימות שבהן היא תלויה ואז עבור כל קבוצה יצרנו אבן דרך שהתלויות שלה זה DEPENDENCIES
//    var groups = _dal.Dependency.ReadAll()
//.OrderBy(dep => dep?.DependsOnTask)
//.GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask,
//(id, dependency) => new { TaskId = id, Dependencies = dependency })
//.Distinct();

//    var groups = _dal.Dependency.ReadAll()
//.OrderBy(dep => dep?.DependsOnTask)
//.GroupBy(
//    dep => dep?.DependentTask,
//    dep => dep?.DependsOnTask,
//     (id, dependencies) => new { TaskId = id, Dependencies = dependencies.ToList(),
//         RelatedTaskIds = dependencies.Distinct().
//         SelectMany(dep => _dal.Dependency.ReadAll()
//         .Where(d => d?.DependsOnTask == dep)
//         .Select(d => d?.DependentTask)).Distinct().ToList() }
//);
// int idDepNext = _dal.Dependency.Create(new DO.Dependency((int)groupDep.TaskId!, idMilstone));//יצירת התלות בין משימה 3 לאבן דרך בתרשים
//var notDependentTasks = _dal.Task.ReadAll().
//    Where(task => task.Id.Equals(groupsByDependentTask.Select(group =>
//    {
//        return Convert.ToInt32(group.TaskId);
//    })));
// var allTaskIds = _dal.Dependency.ReadAll().Select(dep => dep?.DependentTask).Distinct();


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


/*
    var groupedDependencies = _dal.Dependency.ReadAll()
    .OrderBy(dep => dep?.DependsOnTask)
    .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask, (id, dependency) => new { TaskId = id, Dependencies = dependency })
    .ToList();
    */