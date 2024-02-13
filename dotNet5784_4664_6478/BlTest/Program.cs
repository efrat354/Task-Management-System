using BO;
using DalApi;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// Function that resposible to activate the engineer's functions such as read and so on.
    /// </summary>
    /// <exception cref="BlNullPropertyException"></exception>
    /// <exception cref="BlInvalidInput"></exception>
    private static void engineer()
    {
        //Declaration of variables
        int choice = 0;
        int _id, _level;
        string _name, _email;
        double _cost;
        BO.Engineer? engineer;

        Console.WriteLine("Enter 1 to add a new engineer," +
            " 2 to display the engineer by ID" +
            ", 3 to display all the engineers in the company," +
            " 4 to update engineer's details and " +
            "5 to delete or 0 to exit");

        choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));

        //Engineer submenu
        while (choice != 0)
        {
            try
            {
                switch (choice)
                {
                    case 0://exit
                        break;

                    case 1://create
                        Console.WriteLine("Enter engineer's details:");

                        Console.WriteLine("Enter engineer's ID:");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));

                        Console.WriteLine("Enter engineer's name:");
                        _name = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a name"));

                        Console.WriteLine("Enter engineer's email:");
                        _email = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an email"));

                        Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        _level = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a level"));

                        Console.WriteLine("Enter engineer's cost:");
                        _cost = double.Parse(Console.ReadLine()!);

                        engineer = new BO.Engineer()
                        {
                            Id = _id,
                            Name = _name,
                            Email = _email,
                            Level = (BO.EngineerExperience)_level,
                            Cost = _cost
                        };
                        s_bl.Engineer?.Create(engineer);
                        Console.WriteLine("Created successfully");
                        break;

                    case 2://read
                        Console.WriteLine("Enter an Id");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                        engineer = s_bl.Engineer?.Read(_id);
                        Console.WriteLine(engineer);
                        break;

                    case 3://read all
                        var engineersList = s_bl.Engineer?.ReadAll();
                        Console.WriteLine("Engineers details");
                        foreach (BO.Engineer? eng in engineersList!)
                        {
                            Console.WriteLine(eng);
                        }
                        break;

                    case 4://update
                        Console.WriteLine("Enter engineer's details:");

                        Console.WriteLine("Enter engineer's ID:");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));

                        Console.WriteLine("Enter engineer's name:");
                        _name = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a name"));

                        Console.WriteLine("Enter engineer's email:");
                        _email = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an email"));

                        Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        _level = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a level"));

                        Console.WriteLine("Enter engineer's cost:");
                        _cost = double.Parse(Console.ReadLine()!);

                        engineer = new BO.Engineer()
                        {
                            Id = _id,
                            Name = _name,
                            Email = _email,
                            Level = (BO.EngineerExperience)_level,
                            Cost = _cost
                        };
                        s_bl.Engineer?.Update(engineer);
                        Console.WriteLine("successfully updated");
                        break;

                    case 5://delete
                        Console.WriteLine("Enter an Id");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                        s_bl.Engineer?.Delete(_id);
                        Console.WriteLine("Deleted successfully");
                        break;

                    default:
                        throw new BlInvalidInput("Your choice is invalid");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            Console.WriteLine("Enter 1 to add a new engineer," +
                                " 2 to display the engineer by ID" +
                                ", 3 to display all the engineers in the company," +
                                " 4 to update engineer's details and " +
                                "5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
        }
    }

    /// <summary>
    /// Function that resposible to activate the task's functions such as read and so on.
    /// </summary>
    /// <exception cref="BlNullPropertyException"></exception>
    /// <exception cref="BlInvalidInput"></exception>
    private static void task()
    {
        //public List<TaskInList>? Dependencies { get; set; }

        //Declaration of variables
        int choice = 0, _engineerId, _complexityLevel, _id, _status, depOnId = -1, _taskId;
        string _description, _alias, _product, _remarks;
        DateTime _createdAtDate, _scheduledStartDate, _completeDate, _deadlineDate, _startDate;
        TimeSpan _requiredEffortTime;
        BO.Task? task;
        List<TaskInList>? Dependencies;

        Console.WriteLine("Enter 1 to add a new task," +
            " 2 to display the task by ID," +
            " 3 to display all the tasks ," +
            " 4 to update task's details , " +
            "5 to delete or 0 to exit");
        choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));

        while (choice != 0)
        {
            try
            {
                //Task submenu
                switch (choice)
                {
                    case 0://exit
                        break;

                    case 1://create

                        Console.WriteLine("Enter task's details:");

                        Console.WriteLine("Enter task's description:");
                        _description = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a description"));

                        Console.WriteLine("Enter task's alias:");
                        _alias = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an alias"));

                        Console.WriteLine("Enter task's scheduled start date:");
                        _scheduledStartDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter create date"));

                        Console.WriteLine("Enter the amount of time required to perform the task");
                        _requiredEffortTime = new TimeSpan(int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter required effort time")));

                        Console.WriteLine("Enter task's product:");
                        _product = Console.ReadLine()!;

                        Console.WriteLine("Enter task's remarks:");
                        _remarks = Console.ReadLine()!;

                        Console.WriteLine("Enter engineer id:");
                        _engineerId = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an engineer id"));

                        Console.WriteLine("Enter task's complexity level:");
                        _complexityLevel = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a complexity level"));

                        Console.WriteLine("Enter task's dependencies ids ,to finish enter 0:");
                        Dependencies = null;
                        depOnId = int.Parse(Console.ReadLine()!);
                        while (depOnId != 0)
                        {
                            task = s_bl.Task?.Read(depOnId);
                            BO.TaskInList dep = new TaskInList()
                            {
                                Id = depOnId,
                                Alias = task!.Alias,
                                Description = task.Description
                            };
                            if (Dependencies == null)
                            {
                                Dependencies = new List<TaskInList>();
                            }
                            Dependencies.Add(dep);
                            depOnId = int.Parse(Console.ReadLine()!);
                        }

                        task = new BO.Task()
                        {
                            Id = 0,
                            Alias = _alias,
                            Description = _description,
                            CreatedAtDate = DateTime.Now,
                            Status = 0,
                            Dependencies = Dependencies,
                            Milestone = null,
                            ScheduledStartDate = _scheduledStartDate,
                            RequiredEffortTime = _requiredEffortTime,
                            StartDate = null,
                            DeadlineDate = null,
                            CompleteDate = null,
                            Product = _product,
                            Remarks = _remarks,
                            Engineer = new EngineerInTask()
                            {
                                Id = _engineerId,
                                Name = s_bl.Engineer.Read(_engineerId).Name
                            },
                            ComplexityLevel = (BO.EngineerExperience)_complexityLevel,
                        };
                        s_bl.Task.Create(task);
                        Console.WriteLine("Created successfully");
                        break;

                    case 2://read
                        Console.WriteLine("Enter an Id");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                        task = s_bl.Task?.Read(_id);
                        Console.WriteLine(task);
                        break;

                    case 3://read all
                        var taskList = s_bl.Task?.ReadAll();
                        Console.WriteLine("Tasks' details:");
                        foreach (var tk in taskList!)
                        {
                            Console.WriteLine(tk);
                        }
                        break;

                    case 4://update
                        Console.WriteLine("Enter task's details:");

                        Console.WriteLine("Enter task's id:");
                        _taskId = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a description"));

                        Console.WriteLine("Enter task's description:");
                        _description = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a description"));

                        Console.WriteLine("Enter task's alias:");
                        _alias = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an alias"));

                        Console.WriteLine("Enter task's scheduled start date:");
                        _scheduledStartDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter create date"));

                        Console.WriteLine("Enter the amount of time required to perform the task");
                        _requiredEffortTime = new TimeSpan(int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter required effort time")));

                        Console.WriteLine("Enter task's start date:");
                        _startDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Enter task's deadline date:");
                        _deadlineDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Enter task's complete date:");
                        _completeDate = Convert.ToDateTime(Console.ReadLine());

                        Console.WriteLine("Enter task's execution level: 0-Unscheduled, 1-Scheduled, 2-OnTrack, 3-InJeopardy, 4-Done");
                        _status = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Enter task's product:");
                        _product = Console.ReadLine()!;

                        Console.WriteLine("Enter task's remarks:");
                        _remarks = Console.ReadLine()!;

                        Console.WriteLine("Enter engineer id:");
                        _engineerId = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an engineer id"));

                        Console.WriteLine("Enter task's complexity level:");
                        _complexityLevel = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a complexity level"));

                        Console.WriteLine("Enter task's dependencies ids ,to finish enter 0:");
                        Dependencies = null;
                        depOnId = int.Parse(Console.ReadLine()!);
                        while (depOnId != 0)
                        {
                            task = s_bl.Task?.Read(depOnId);
                            Dependencies?.Add(new TaskInList()
                            {
                                Id = depOnId,
                                Alias = task!.Alias,
                                Description = task.Description
                            });
                            depOnId = int.Parse(Console.ReadLine()!);
                        }

                        task = new BO.Task()
                        {
                            Id = _taskId,
                            Alias = _alias,
                            Description = _description,
                            CreatedAtDate = DateTime.Now,//לשנות לתאריך האמיתי שלו
                            Status = (Status)_status,
                            Dependencies = Dependencies,
                            Milestone = null,
                            ScheduledStartDate = _scheduledStartDate,
                            RequiredEffortTime = _requiredEffortTime,
                            StartDate = _startDate,
                            DeadlineDate = _deadlineDate,
                            CompleteDate = _completeDate,
                            Product = _product,
                            Remarks = _remarks,
                            Engineer = new EngineerInTask()
                            {
                                Id = _engineerId,
                                Name = s_bl.Engineer.Read(_engineerId).Name
                            },
                            ComplexityLevel = (BO.EngineerExperience)_complexityLevel
                        };

                        s_bl.Task?.Update(task);
                        Console.WriteLine("successfully updated");
                        break;

                    case 5://delete
                        Console.WriteLine("Enter task's id");
                        _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                        s_bl.Task?.Delete(_id);
                        Console.WriteLine("Deleted successfully");
                        break;

                    default:
                        throw new BlInvalidInput("Your choice is invalid");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            Console.WriteLine("Enter 1 to add a new task," +
                                " 2 to display the task by ID," +
                                " 3 to display all the tasks ," +
                                " 4 to update task's details , " +
                                "5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
        }
    }

    /// <summary>
    /// Function that resposible to activate the milstone's functions in order to create the project's schedule.
    /// </summary>
    private static void milstone()
    {
        s_bl.Milestone.CreateSchedule();
    }


    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new BlNullPropertyException("No choice was made");

        if (ans == "Y")
        {
            Factory.Get.Reset();
            DalTest.Initialization.Do();
        }

        try
        {
            int choice = 0;
            Console.WriteLine("Enter 1 to engineer," +
                " 2 to task , " +
                "3 to create schedule " +
                "or 0 to exit");

            choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));

            //The main menu
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1://engineer
                        engineer();
                        break;

                    case 2://task
                        try
                        {
                            task();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 3://milstone
                        try
                        {
                            milstone();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        throw new BlNullPropertyException("your choice is invalid");
                }
                Console.WriteLine("Enter 1 to engineer," +
                    " 2 to task , " +
                    "3 to create schedule " +
                    "or 0 to exit");

                choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}




