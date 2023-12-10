using Dal;
using DalApi;
using DO;

namespace DalTest
{
    //Running the project
    internal class Program
    {
        //Enabling access to the global interface we defined
        static readonly IDal s_dal = new DalList();

        //Function that manage all the functions of engineer
        private static void engineer()
        {
            //Declaration of variables
            int choice = 0;
            int _id, _level;
            string _name, _email;
            double _cost;
            Engineer? engineer;

            Console.WriteLine("Enter 1 to add a new engineer, 2 to display the engineer by ID, 3 to display all the engineers in the company, 4 to update engineer's details and 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a choice"));
            //Engineer submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter engineer's details:");
                    Console.WriteLine("Enter engineer's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    Console.WriteLine("Enter engineer's name:");
                    _name = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a name"));
                    Console.WriteLine("Enter engineer's email:");
                    _email = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an email"));
                    Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                    _level = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a level"));
                    Console.WriteLine("Enter engineer's cost:");
                    _cost = double.Parse(Console.ReadLine()!);
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dal.Engineer?.Create(engineer);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    engineer= s_dal.Engineer?.Read(_id);
                    if(engineer != null)
                    {
                        Console.WriteLine( engineer);
                    }
                    else
                    Console.WriteLine( "not exist");
                    break;
                case 3://read all
                    var engineersList = s_dal.Engineer?.ReadAll();
                    Console.WriteLine( "Engineers details");
                    foreach (Engineer? eng in engineersList!)
                    {
                        Console.WriteLine(eng);
                    }
                    break;
                case 4://update
                    Console.WriteLine("Enter engineer's details:");
                    Console.WriteLine("Enter engineer's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    Console.WriteLine("Enter engineer's name:");
                    _name = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a name"));
                    Console.WriteLine("Enter engineer's email:");
                    _email = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an email"));
                    Console.WriteLine("Enter engineer's level { 0-Novice, 1-AdvancedBeginner, 2-Competent, 3-Proficient, 4-Expert }:");
                    _level = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a level"));
                    Console.WriteLine("Enter engineer's cost:");
                    _cost = double.Parse(Console.ReadLine()!);
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dal.Engineer?.Update(engineer);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    s_dal.Engineer?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new DalInvalidInput("Your choice is invalid");
            }
        }

        //Function that manage all the functions of task
        private static void task()
        {
            //DateTime _createdAt, _start, _forcastDate, _deadline, _complete;נעשה שינויים בשמות
            //Declaration of variables
            int choice = 0, _engineerId, _complexity,_id;
            string _description, _alias, _product, _remarks;
            DateTime _createdAtDate, _startDate, _scheduledDate, _completeDate;
            TimeSpan _requiredEffortTime;
            DO.Task? task;
            Console.WriteLine("Enter 1 to add a new task, 2 to display the task by ID, 3 to display all the tasks , 4 to update task's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a choice"));
            //Task submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter task's details:");
                    Console.WriteLine("Enter task's description:");
                    _description = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a description"));
                    Console.WriteLine("Enter task's alias:");
                    _alias = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an alias"));
                    Console.WriteLine("Enter task's create date:");
                    _createdAtDate = Convert.ToDateTime(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter create date"));
                    Console.WriteLine("Enter the amount of time required to perform the task");
                    _requiredEffortTime=new TimeSpan(int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter required effort time")));
                    Console.WriteLine("Enter task's start date:");
                    _startDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's forcast date:");
                    _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's deadline date:");
                    //_deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's complete date:");
                    _completeDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's product:");
                    _product = Console.ReadLine()!;
                    Console.WriteLine("Enter task's remarks:");
                    _remarks = Console.ReadLine()!;
                    Console.WriteLine("Enter engineer id:");
                    _engineerId = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an engineer id"));
                    Console.WriteLine("Enter task's complexity level:");
                    _complexity = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a complexity level"));
                    task = new DO.Task(0, _alias, _description, _createdAtDate, _requiredEffortTime,false, (EngineerExperience)_complexity, _startDate, _scheduledDate,null, _completeDate, _product, _remarks, _engineerId);
                    s_dal.Task?.Create(task);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    task = s_dal.Task!.Read(_id);
                    if (task!= null)
                    {
                        Console.WriteLine(task);
                    }
                    else
                        Console.WriteLine("not exist");
                    break;
                case 3://read all
                    var taskList = s_dal.Task?.ReadAll();
                    Console.WriteLine("Engineers details");
                    foreach (DO.Task ?tk in taskList!)
                    {
                        Console.WriteLine(tk);
                    }
                    break;
                case 4://update
                    Console.WriteLine("Enter task's details:");
                    Console.WriteLine("Enter task's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    Console.WriteLine("Enter task's description:");
                    _description = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a description"));
                    Console.WriteLine("Enter task's alias:");
                    _alias = (Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an alias"));
                    Console.WriteLine("Enter task's create date:");
                    _createdAtDate = Convert.ToDateTime(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter create date"));
                    Console.WriteLine("Enter the amount of time required to perform the task");
                    _requiredEffortTime = new TimeSpan(int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter required effort time")));
                    Console.WriteLine("Enter task's start date:");
                    _startDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's forcast date:");
                    _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's deadline date:");
                   // _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's complete date:");
                    _completeDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's product:");
                    _product = Console.ReadLine()!;
                    Console.WriteLine("Enter task's remarks:");
                    _remarks = Console.ReadLine()!;
                    Console.WriteLine("Enter engineer id:");
                    _engineerId = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an engineer id"));
                    Console.WriteLine("Enter task's complexity level:");
                    _complexity = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a complexity level"));
                    task = new DO.Task(0, _alias, _description, _createdAtDate, _requiredEffortTime, false, (EngineerExperience)_complexity, _startDate, _scheduledDate, null, _completeDate, _product, _remarks, _engineerId);
                    s_dal.Task?.Update(task);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    s_dal.Task?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new DalInvalidInput("Your choice is invalid");
            }
        }
        
        //Function that manage all the functions of dependency
        private static void dependency()
        {
            //Declaration of variables
            Dependency ?dependency;
            int _id, _dependentTask, _dependsOnTask, choice;
            Console.WriteLine("Enter 1 to add a new dependency, 2 to display a dependency by ID, 3 to display all the dependency , 4 to update dependency's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a choice"));
            //Dependency submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter a number of DependentTask");
                    _dependentTask = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("you did not enter dependent task"));
                    Console.WriteLine("Enter number of DependsOnTask");
                    _dependsOnTask = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("you did not enter _depends on task"));
                    dependency = new Dependency(0, _dependentTask, _dependsOnTask);
                    s_dal.Dependency?.Create(dependency);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    dependency = s_dal.Dependency?.Read(_id);
                    if (dependency != null)
                    {
                        Console.WriteLine(dependency);
                    }
                    else
                        Console.WriteLine("not exist");
                     break;
                case 3://read all
                    var dependencyList = s_dal.Dependency?.ReadAll();
                    Console.WriteLine("Engineers details");
                    foreach (Dependency? dep in dependencyList!)
                    {
                        Console.WriteLine(dep);
                    }
                    break;
                case 4://update
                    Console.WriteLine("Enter dependency's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    Console.WriteLine("Enter a number of DependentTask");
                    _dependentTask = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("you did not enter dependent task"));
                    Console.WriteLine("Enter number of DependsOnTask");
                    _dependsOnTask = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("you did not enter _depends on task"));
                    dependency = new Dependency(_id, _dependentTask, _dependsOnTask);
                    s_dal.Dependency?.Update(dependency);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter an id"));
                    s_dal.Dependency?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new DalInvalidInput("Your choice is invalid");
            }
        }

        //Project management
        static void Main(string[] args)
        {
            try
            {
                int choice = 0;
                //Intilizate the project with some deta.
                Initialization.Do(s_dal);
                Console.WriteLine("Enter 1 to engineer, 2 to task and 3 to dependency or 0 to exit");
                choice = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a choice"));
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
                        default:
                            throw new DalInvalidInput("your choice is invalid");
                    }
                    Console.WriteLine("Enter 1 to engineer, 2 to task and 3 to dependency or 0 to exit");
                    choice = int.Parse(Console.ReadLine() ?? throw new DalInvalidInput("You did not enter a choice"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}