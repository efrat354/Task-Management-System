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

        Console.WriteLine("Enter 1 to add a new engineer, 2 to display the engineer by ID" +
            ", 3 to display all the engineers in the company, 4 to update engineer's details and 5 to delete or 0 to exit");
        choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
       
        //Engineer submenu
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
                    Id= _id, 
                    Name=_name, 
                    Email=_email,
                    Level=(BO.EngineerExperience)_level,
                    Cost=_cost 
                };
                s_bl.Engineer.Create(engineer);
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
                s_bl.Engineer.Update(engineer);
                Console.WriteLine("successfully updated");
                break;

            case 5://delete
                Console.WriteLine("Enter an Id");
                _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                s_bl.Engineer.Delete(_id);
                Console.WriteLine("Deleted successfully");
                break;

            default:
                throw new BlInvalidInput("Your choice is invalid");
        }
    }

    /// <summary>
    /// Function that resposible to activate the task's functions such as read and so on.
    /// </summary>
    /// <exception cref="BlNullPropertyException"></exception>
    /// <exception cref="BlInvalidInput"></exception>
    private static void task()
    {
   
    //public Status? Status { get; set; }
    //public List<TaskInList>? Dependencies { get; set; }
    //public Milestone? Milestone { get; set; }
    //public EngineerInTask? Engineer { get; set; }//????

    //Declaration of variables
    int choice = 0, _engineerId, _complexityLevel, _id;
        string _description, _alias, _product, _remarks;
        DateTime _createdAtDate, _startDate, _forecastDate, _completeDate, _deadlineDate, _scheduledStartDate;
        TimeSpan _requiredEffortTime;
        BO.Task? task;
        Console.WriteLine("Enter 1 to add a new task, 2 to display the task by ID, 3 to display all the tasks , 4 to update task's details , 5 to delete or 0 to exit");
        choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
        
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
                Console.WriteLine("Enter task's create date:");
                _createdAtDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter scheduled start date"));
                Console.WriteLine("Enter task's scheduled start date:");
                _scheduledStartDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter create date"));
                Console.WriteLine("Enter the amount of time required to perform the task");
                _requiredEffortTime = new TimeSpan(int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter required effort time")));
                Console.WriteLine("Enter task's start date:");
                _startDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's scheduled date:");
                _forecastDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's deadline date:");
                _deadlineDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's complete date:");
                _completeDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's product:");
                _product = Console.ReadLine()!;
                Console.WriteLine("Enter task's remarks:");
                _remarks = Console.ReadLine()!;
                Console.WriteLine("Enter engineer id:");
                _engineerId = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an engineer id"));
                Console.WriteLine("Enter task's complexity level:");
                _complexityLevel = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a complexity level"));
                task = new BO.Task()
                {
                    Id=0,
                    Alias=_alias, 
                    Description=_description,
                    CreatedAtDate=_createdAtDate,
                    Status=0,
                    //dependencies
                    //milstone
                    ScheduledStartDate= _scheduledStartDate,
                    RequiredEffortTime =_requiredEffortTime, 
                    StartDate= _startDate,
                    ForecastDate= _forecastDate,
                    DeadlineDate= _deadlineDate,
                    CompleteDate= _completeDate,
                    Product= _product,
                    Remarks= _remarks,
                    //engineer
                    ComplexityLevel= (BO.EngineerExperience)_complexityLevel,
                };
                s_bl.Task?.Create(task);
                Console.WriteLine("Created successfully");
                break;

            case 2://read
                Console.WriteLine("Enter an Id");
                _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                task = s_bl.Task.Read(_id);
                Console.WriteLine(task);
                break;

            case 3://read all
                var taskList = s_bl.Task.ReadAll();
                Console.WriteLine("Engineers details:");
                foreach (BO.Task? tk in taskList!)
                {
                    Console.WriteLine(tk);
                }
                break;

            case 4://update
                Console.WriteLine("Enter task's details:");

                Console.WriteLine("Enter task's description:");
                _description = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a description"));
                Console.WriteLine("Enter task's alias:");
                _alias = (Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an alias"));
                Console.WriteLine("Enter task's create date:");
                _createdAtDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter scheduled start date"));
                Console.WriteLine("Enter task's scheduled start date:");
                _scheduledStartDate = Convert.ToDateTime(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter create date"));
                Console.WriteLine("Enter the amount of time required to perform the task");
                _requiredEffortTime = new TimeSpan(int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter required effort time")));
                Console.WriteLine("Enter task's start date:");
                _startDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's scheduled date:");
                _forecastDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's deadline date:");
                _deadlineDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's complete date:");
                _completeDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter task's product:");
                _product = Console.ReadLine()!;
                Console.WriteLine("Enter task's remarks:");
                _remarks = Console.ReadLine()!;
                Console.WriteLine("Enter engineer id:");
                _engineerId = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an engineer id"));
                Console.WriteLine("Enter task's complexity level:");
                _complexityLevel = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a complexity level"));
                task = new BO.Task()
                {
                    Id = 0,
                    Alias = _alias,
                    Description = _description,
                    CreatedAtDate = _createdAtDate,
                    Status = 0,
                    //dependencies
                    //milstone
                    ScheduledStartDate = _scheduledStartDate,
                    RequiredEffortTime = _requiredEffortTime,
                    StartDate = _startDate,
                    ForecastDate = _forecastDate,
                    DeadlineDate = _deadlineDate,
                    CompleteDate = _completeDate,
                    Product = _product,
                    Remarks = _remarks,
                    //engineer
                    ComplexityLevel = (BO.EngineerExperience)_complexityLevel,
                };
                s_bl.Task.Update(task);
                Console.WriteLine("successfully updated");
                break;

            case 5://delete
                Console.WriteLine("Enter task's id");
                _id = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter an id"));
                s_bl.Task.Delete(_id);
                Console.WriteLine("Deleted successfully");
                break;

            default:
                throw new BlInvalidInput("Your choice is invalid");
        }
    }

    /// <summary>
    /// Function that resposible to activate the milstone's functions in order to create the project's schedule.
    /// </summary>
    private static void milstone()
    {

    }


    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new BlNullPropertyException("No choice was made");
        if (ans == "Y")
        {
            //זימון RESET
            Factory.Get.Reset();
            DalTest.Initialization.Do();

        }

        try
        {
            int choice = 0;
            Console.WriteLine("Enter 1 to engineer, 2 to task , 3 to create schedule or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
            
            //The main menu
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1://engineer
                        try
                        {
                            engineer();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
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
                Console.WriteLine("Enter 1 to engineer, 2 to task , 3 to create schedule or 0 to exit");
                choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
   



namespace DalTest
{
    //Running the project
    internal class Program
    {
        //Enabling access to the global interface we defined
        //static readonly IDal s_dal = new DalList();
        //static readonly IDal s_dal = new DalXml(); //stage 3
        static readonly IDal s_dal = Factory.Get;

        //Project management
        static void Main(string[] args)
        {
            try
            {
                int choice = 0;
                //Intilizate the project with some deta.
                Console.WriteLine("Enter 1 to engineer, 2 to task , 3 to dependency and 4 to initialize data or 0 to exit");
                choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
                //The main menu
                while (choice != 0)
                {
                    switch (choice)
                    {
                        case 1://engineer
                            try
                            {
                                engineer();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
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
                        case 3://dependency
                            try
                            {
                                dependency();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 4:
                            Console.Write("Would you like to create Initial data? (Y/N)");
                            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
                            if (ans == "Y")
                            {
                                s_dal.Reset();
                                Initialization.Do();
                            }
                            break;
                        default:
                            throw new BlNullPropertyException("your choice is invalid");
                    }
                    Console.WriteLine("Enter 1 to engineer, 2 to task , 3 to dependency and 4 to initialize data or 0 to exit");
                    choice = int.Parse(Console.ReadLine() ?? throw new BlNullPropertyException("You did not enter a choice"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}