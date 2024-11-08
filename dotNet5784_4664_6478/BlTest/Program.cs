﻿using BO;
using DalApi;
/// <summary>
/// The program responsible to introduce all the BL layer- task, engineer, milestone
/// </summary>
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
   /// <summary>
   /// Function that include all the engineer's functions- create, update and so on.
   /// </summary>
   /// <exception cref="BlNullPropertyException">When the user doesn't enter a value</exception>
   /// <exception cref="BlInvalidInput">When the user enter invalid input</exception>
    private static void engineer()
    {
        int choice = 0;
        int _id, _level;
        string _name, _email;
        double _cost;
        BO.Engineer? engineer;

        Console.WriteLine("Enter 1 to add a new engineer,\n" +
            " 2 to display the engineer by ID\n" +
            ", 3 to display all the engineers in the company,\n" +
            " 4 to update engineer's details \n" +
            "5 to delete or 0 to exit\n");

        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                throw new BlNullPropertyException("Invalid input. Please enter a valid choice.");
            }

            if (choice == 0)
                break;

            try
            {
                switch (choice)
                {
                    case 1://Create an engineer
                        Console.WriteLine("Enter engineer's details:");

                        Console.WriteLine("Enter engineer's ID:");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }

                        Console.WriteLine("Enter engineer's name:");
                        _name = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid name.");

                        Console.WriteLine("Enter engineer's email:");
                        _email = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid email.");

                        Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        if (!int.TryParse(Console.ReadLine(), out _level))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid level.");
                        }

                        Console.WriteLine("Enter engineer's cost:");
                        if (!double.TryParse(Console.ReadLine(), out _cost))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid cost.");
                        }

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

                    case 2://Read an Engineer
                        Console.WriteLine("Enter an Id");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }
                        engineer = s_bl.Engineer?.Read(_id);
                        Console.WriteLine(engineer);
                        break;

                    case 3://Read all the engineers
                        var engineersList = s_bl.Engineer?.ReadAll();
                        Console.WriteLine("Engineers details");
                        foreach (BO.Engineer? eng in engineersList!)
                        {
                            Console.WriteLine(eng);
                        }
                        break;

                    case 4://Update an engineer
                        Console.WriteLine("Enter engineer's details:");

                        Console.WriteLine("Enter engineer's ID:");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }

                        Console.WriteLine("Enter engineer's name:");
                        _name = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid name.");

                        Console.WriteLine("Enter engineer's email:");
                        _email = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid email.");

                        Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        if (!int.TryParse(Console.ReadLine(), out _level))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid level.");
                        }

                        Console.WriteLine("Enter engineer's cost:");
                        if (!double.TryParse(Console.ReadLine(), out _cost))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid cost.");
                        }

                        engineer = new BO.Engineer()
                        {
                            Id = _id,
                            Name = _name,
                            Email = _email,
                            Level = (BO.EngineerExperience)_level,
                            Cost = _cost
                        };
                        s_bl.Engineer?.Update(engineer);
                        Console.WriteLine("Successfully updated");
                        break;

                    case 5://Delete an engineer
                        Console.WriteLine("Enter an Id");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }
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
            Console.WriteLine("Enter 1 to add a new engineer,\n" +
             " 2 to display the engineer by ID\n" +
             ", 3 to display all the engineers in the company,\n" +
             " 4 to update engineer's details \n" +
             "5 to delete or 0 to exit\n");
        }
    }

    /// <summary>
    /// Function that include all the task's functions- create, update and so on.
    /// </summary>
    /// <exception cref="BlNullPropertyException">When the user doesn't enter a value</exception>
    /// <exception cref="BlInvalidInput">When the user enter invalid input</exception>
    private static void task()
    {
        int choice = 0, _engineerId, _complexityLevel, _id, _status, depOnId = -1, _taskId, _requiredEffortTime;
        string _description, _alias, _product, _remarks;
        DateTime _scheduledStartDate, _completeDate, _deadlineDate, _startDate;
        BO.Task? task;
        List<TaskInList>? Dependencies;

        Console.WriteLine("Enter 1 to add a new task,\n" +
            " 2 to display the task by ID,\n" +
            " 3 to display all the tasks ,\n" +
            " 4 to update task's details , \n" +
            "5 to delete or 0 to exit\n");
        choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));

        while (choice != 0)
        {
            try
            {
                switch (choice)
                {
                    case 1://Create a task
                        Console.WriteLine("Enter task's details:");

                        Console.WriteLine("Enter task's description:");
                        _description = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid description.");

                        Console.WriteLine("Enter task's alias:");
                        _alias = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid alias.");

                        Console.WriteLine("Enter task's scheduled start date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out _scheduledStartDate))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid scheduled start date.");
                        }

                        Console.WriteLine("Enter the amount of time required to perform the task");
                        if (!int.TryParse(Console.ReadLine(), out _requiredEffortTime))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid required effort time.");
                        }

                        Console.WriteLine("Enter task's product:");
                        _product = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid product.");

                        Console.WriteLine("Enter task's remarks:");
                        _remarks = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter valid remarks.");

                        Console.WriteLine("Enter engineer id:");
                        if (!int.TryParse(Console.ReadLine(), out _engineerId))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid engineer id.");
                        }

                        Console.WriteLine("Enter task's complexity level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        if (!int.TryParse(Console.ReadLine(), out _complexityLevel))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid complexity level.");
                        }

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
                            RequiredEffortTime = new TimeSpan(_requiredEffortTime),
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
                        s_bl.Task?.Create(task);
                        Console.WriteLine("Created successfully");
                        break;
                        
                    case 2://Read a task
                        Console.WriteLine("Enter an Id");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }
                        task = s_bl.Task?.Read(_id);
                        Console.WriteLine(task);
                        break;

                    case 3://Read all the tasks
                        var taskList = s_bl.Task?.ReadAll();
                        Console.WriteLine("Tasks' details:");
                        foreach (var tk in taskList!)
                        {
                            Console.WriteLine(tk);
                        }
                        break;

                    case 4://Update a task
                        Console.WriteLine("Enter task's details:");

                        Console.WriteLine("Enter task's id:");
                        if (!int.TryParse(Console.ReadLine(), out _taskId))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }

                        Console.WriteLine("Enter task's description:");
                        _description = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid description.");

                        Console.WriteLine("Enter task's alias:");
                        _alias = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid alias.");

                        Console.WriteLine("Enter task's scheduled start date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out _scheduledStartDate))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid scheduled start date.");
                        }

                        Console.WriteLine("Enter the amount of time required to perform the task");
                        if (!int.TryParse(Console.ReadLine(), out _requiredEffortTime))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid required effort time.");
                        }

                        Console.WriteLine("Enter task's start date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out _startDate))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid start date.");
                        }

                        Console.WriteLine("Enter task's deadline date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out _deadlineDate))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid deadline date.");
                        }

                        Console.WriteLine("Enter task's complete date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out _completeDate))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid complete date.");
                        }

                        Console.WriteLine("Enter task's execution level: 0-Unscheduled, 1-Scheduled, 2-OnTrack, 3-InJeopardy, 4-Done");
                        if (!int.TryParse(Console.ReadLine(), out _status))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid execution level.");
                        }

                        Console.WriteLine("Enter task's product:");
                        _product = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter a valid product.");

                        Console.WriteLine("Enter task's remarks:");
                        _remarks = Console.ReadLine() ?? throw new BlNullPropertyException("Invalid input. Please enter valid remarks.");

                        Console.WriteLine("Enter engineer id:");
                        if (!int.TryParse(Console.ReadLine(), out _engineerId))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid engineer id.");
                        }

                        Console.WriteLine("Enter task's complexity level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                        if (!int.TryParse(Console.ReadLine(), out _complexityLevel))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid complexity level.");
                        }

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
                            Id = _taskId,
                            Alias = _alias,
                            Description = _description,
                            CreatedAtDate = DateTime.Now,
                            Status = (Status)_status,
                            Dependencies = Dependencies,
                            Milestone = null,
                            ScheduledStartDate = _scheduledStartDate,
                            RequiredEffortTime = new TimeSpan(_requiredEffortTime),
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

                    case 5://Delete a task
                        Console.WriteLine("Enter task's id");
                        if (!int.TryParse(Console.ReadLine(), out _id))
                        {
                            throw new BlNullPropertyException("Invalid input. Please enter a valid ID.");
                        }
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
            Console.WriteLine("Enter 1 to add a new task,\n" +
           " 2 to display the task by ID,\n" +
           " 3 to display all the tasks ,\n" +
           " 4 to update task's details , \n" +
           "5 to delete or 0 to exit\n");
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                throw new BlNullPropertyException("Invalid input. Please enter a valid choice.");
            }
        }
    }
    /// <summary>
    /// Function that cause to create the schedule of the whole project - determines the order of execution of tasks and their dates
    /// </summary>
    private static void milestone()
    {
        s_bl.Milestone.CreateSchedule();
        Console.WriteLine("The schedule created successfully");
    }
    /// <summary>
    /// The main function that navigates the user to the functions of each entity
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="BlNullPropertyException">When the user doesn't enter a value</exception>
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
            Console.WriteLine("Enter 1 to engineer,\n" +
                " 2 to task ,\n " +
                "3 to create schedule\n " +
                "or 0 to exit\n");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                throw new BlNullPropertyException("Invalid input. Please enter a valid choice.");
            }

            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        engineer();
                        break;

                    case 2:
                        try
                        {
                            task();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            milestone();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        throw new BlNullPropertyException("your choice is invalid");
                }
                Console.WriteLine("Enter 1 to engineer,\n" +
                    " 2 to task , \n" +
                    "3 to create schedule \n" +
                    "or 0 to exit\n");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    throw new BlNullPropertyException("Invalid input. Please enter a valid choice.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}