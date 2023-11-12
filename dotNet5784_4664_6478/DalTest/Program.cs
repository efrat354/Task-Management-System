using Dal;
using DalApi;
using DO;
using System.Globalization;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace DalTest
{
    internal class Program
    {
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static void engineer()
        {
            int choice = 0;
            int _id, _level;
            string _name, _email;
            double _cost;
            Engineer engineer;
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            Console.WriteLine("Enter 1 to add a new engineer, 2 to display the engineer by ID, 3 to display all the engineers in the company, 4 to update engineer's details and 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Enter engineer's details: ID, name, email,level (0-JR,1-rookie,2-expert)");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    _name = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    _email = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    _level = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an level"));
                    _cost = double.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dalEngineer?.Create(engineer);
                    break;
                case 2:
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Read(_id);
                    break;
                case 3:
                    Console.WriteLine(s_dalEngineer?.ReadAll());
                    break;
                case 4:
                    Console.WriteLine("Enter engineer's details: ID, name, email,level (0-JR,1-rookie,2-expert)");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    _name = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    _email = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    _level = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an level"));
                    _cost = double.Parse(Console.ReadLine()!);
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dalEngineer?.Update(engineer);
                    break;
                case 5:
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Delete(_id);
                    break;
                default:
                    throw new Exception("Your choice is invalid");
            }

        }
        private static void task()
        {
            //        bool Milestone = false,
            //DateTime CreatedAt = new DateTime() ,
            //DateTime? Start = null,
            //DateTime? ForcastDate = null,
            //DateTime? Deadline = null,
            //DateTime? Complete = null  ,
            //string? Product = null,
            //string? Remarks = null,
            //int? EngineerId = null,
            //EngineerExperience ComplexityLevel = 0
            int choice = 0;
            string _description, _alias;
        
            DO.Task task;
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            Console.WriteLine("Enter 1 to add a new task, 2 to display the task by ID, 3 to display all the tasks , 4 to update task's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            DateTime _createdAt, _start, _forcastDate, _deadline, _complete;
            string _product, _remarks;
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Enter task's details:description and alias");
                    _description = (Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    _alias = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    Console.WriteLine("Enter task's details:description and alias");
                    _createdAt = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                     _start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _forcastDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _product = Console.ReadLine();
                    Console.WriteLine("Enter task's details:description and alias");
                    _remarks=Console.ReadLine();
                    task = new DO.Task(0,_description, _alias, false, _createdAt, _start, _forcastDate, _deadline, _complete, _product, _remarks);
                    s_dalTask?.Create(task);
                    break;
                case 2:
                    int _id;
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Read(_id);
                    break;
                case 3:
                    s_dalEngineer?.ReadAll();
                    break;
                case 4://update
                    Console.WriteLine("Enter task's details:description and alias");
                    _description = (Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    _alias = (Console.ReadLine() ?? throw new Exception("You did not enter an"));
                    task = new DO.Task();
                    Console.WriteLine("Enter task's details:description and alias");
                    _start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _forcastDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's details:description and alias");
                    _product = Console.ReadLine();
                    Console.WriteLine("Enter task's details:description and alias");
                    _remarks = Console.ReadLine();
                    task = new DO.Task(0, _description, _alias, false, _createdAt, _start, _forcastDate, _deadline, _complete, _product, _remarks);
                     s_dalTask?.Update(task);
                    break;
                case 5:
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalTask?.Delete(_id);
                    break;
                default:
                    throw new Exception("Your choice is invalid");
            }

        }
        private static void dependecy()
        {
            int choice = 0;
            int DependentTask, DependsOnTask;
            Dependency dependency;
            Console.WriteLine("Enter 1 to add a new dependency, 2 to display a dependency by ID, 3 to display all the dependency , 4 to update dependency's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            switch (choice)
            {
                case 0:
                    break;
            }

        }

        static void Main(string[] args)
        {
            try
            {
                int choice = 0;
                Initialization.Do(s_dalEngineer, s_dalDependency, s_dalTask);
                Console.WriteLine("Enter 1 to engineer, 2 to task and 3 to dependency or 0 to exit");
                choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
                //the main menu
                while (choice != 0)
                {

                    switch (choice)
                    {

                        case 1://engineer
                           try { 
                             engineer();
                           }
                           catch (Exception e)
                           {
                                Console.WriteLine(e.Message);
                           }
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
                             dependecy();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        default:
                            throw new Exception("your choice is invaild");

                    }
                    choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }


}