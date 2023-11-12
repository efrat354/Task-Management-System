﻿using Dal;
using DalApi;
using DO;
using System.Globalization;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Xml.Linq;
//האם צריך להגדיר משתנים לכל תכונה ואז להכניס אותם בפעולה בונה או קודם כל ליצור את העצם ואז לשוח את הנתונים ישר לתוך התכונות
//לבדוק את כל סימני הקריאה והסימני שאלה
//למה רק בTASK הוא מחייב לעשות DO.TASK
//למה ?? לא עובדים
namespace DalTest
{
    //Running the project
    internal class Program
    {
        //Enabling access to the interfaces we defined
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); 
        private static IDependency? s_dalDependency = new DependencyImplementation(); 
        private static ITask? s_dalTask = new TaskImplementation(); 

       //Function that manage all the functions of engineer
        private static void engineer()
        {
            //Declaration of variables
            int choice = 0;
            int _id, _level;
            string _name, _email;
            double _cost;
            Engineer engineer;

            Console.WriteLine("Enter 1 to add a new engineer, 2 to display the engineer by ID, 3 to display all the engineers in the company, 4 to update engineer's details and 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            //Engineer submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter engineer's details:");
                    Console.WriteLine("Enter engineer's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    Console.WriteLine("Enter engineer's name:");
                    _name = (Console.ReadLine() ?? throw new Exception("You did not enter a name"));
                    Console.WriteLine("Enter engineer's email:");
                    _email = (Console.ReadLine() ?? throw new Exception("You did not enter an email"));
                    Console.WriteLine("Enter engineer's level (0-JR,1-rookie,2-expert):");
                    _level = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a level"));
                    Console.WriteLine("Enter engineer's cost:");
                    _cost = double.Parse(Console.ReadLine()!);
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dalEngineer?.Create(engineer);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Read(_id);
                    break;
                case 3://read all
                    Console.WriteLine(s_dalEngineer?.ReadAll());
                    break;
                case 4://update
                    Console.WriteLine("Enter engineer's details:");
                    Console.WriteLine("Enter engineer's ID:");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    Console.WriteLine("Enter engineer's name:");
                    _name = (Console.ReadLine() ?? throw new Exception("You did not enter a name"));
                    Console.WriteLine("Enter engineer's email:");
                    _email = (Console.ReadLine() ?? throw new Exception("You did not enter an email"));
                    Console.WriteLine("Enter engineer's level (0-JR,1-rookie,2-expert):");
                    _level = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a level"));
                    Console.WriteLine("Enter engineer's cost:");
                    _cost = double.Parse(Console.ReadLine()!);
                    engineer = new Engineer(_id, _name, _email, (EngineerExperience)_level, _cost);
                    s_dalEngineer?.Update(engineer);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new Exception("Your choice is invalid");
            }
        }

        //Function that manage all the functions of task
        private static void task()
        {
            //Declaration of variables
            int choice = 0, _engineerId, _complexityLevel;
            string _description, _alias, _product, _remarks;
            DateTime _createdAt, _start, _forcastDate, _deadline, _complete;
            DO.Task task;

            Console.WriteLine("Enter 1 to add a new task, 2 to display the task by ID, 3 to display all the tasks , 4 to update task's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            //Task submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter task's details:");
                    Console.WriteLine("Enter task's description:");
                    _description = (Console.ReadLine() ?? throw new Exception("You did not enter a description"));
                    Console.WriteLine("Enter task's alias:");
                    _alias = (Console.ReadLine() ?? throw new Exception("You did not enter an alias"));
                    Console.WriteLine("Enter task's create date:");
                    _createdAt = Convert.ToDateTime(Console.ReadLine() ?? throw new Exception("You did not enter create date"));
                    Console.WriteLine("Enter task's start date:");
                    _start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's forcast date:");
                    _forcastDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's deadline date:");
                    _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's complete date:");
                    _complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's product:");
                    _product = Console.ReadLine()!;
                    Console.WriteLine("Enter task's remarks:");
                    _remarks = Console.ReadLine()!;
                    Console.WriteLine("Enter engineer id:");
                    _engineerId=int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an engineer id"));
                    Console.WriteLine("Enter task's complexity level:");
                    _complexityLevel=int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a complexity level"));
                    task = new DO.Task(0,_description, _alias, false, _createdAt, _start, _forcastDate, _deadline, _complete, _product, _remarks, _engineerId, (EngineerExperience)_complexityLevel);
                    s_dalTask?.Create(task);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    int _id;
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalEngineer?.Read(_id);
                    break;
                case 3://read all
                    s_dalEngineer?.ReadAll();
                    break;
                case 4://update
                    Console.WriteLine("Enter task's details:");
                    Console.WriteLine("Enter task's description:");
                    _description = (Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    Console.WriteLine("Enter task's alias:");
                    _alias = (Console.ReadLine() ?? throw new Exception("You did not enter an alias"));
                    Console.WriteLine("Enter task's create date:");
                    _createdAt = Convert.ToDateTime(Console.ReadLine() ?? throw new Exception("You did not enter create date"));
                    Console.WriteLine("Enter task's start date:");
                    _start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's forcast date:");
                    _forcastDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's deadline date:");
                    _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's complete date:");
                    _complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter task's product:");
                    _product = Console.ReadLine()!;
                    Console.WriteLine("Enter task's remarks:");
                    _remarks = Console.ReadLine()!;
                    Console.WriteLine("Enter engineer id:");
                    _engineerId = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an engineer id"));
                    Console.WriteLine("Enter task's complexity level:");
                    _complexityLevel = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a complexity level"));
                    task = new DO.Task(0, _description, _alias, false, _createdAt, _start, _forcastDate, _deadline, _complete, _product, _remarks, _engineerId, (EngineerExperience)_complexityLevel);
                    s_dalTask?.Update(task);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalTask?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new Exception("Your choice is invalid");
            }

        }

        //Function that manage all the functions of dependency
        private static void dependecy()
        {
            //Declaration of variables
            int choice = 0;
            int _id, _dependentTask, _dependsOnTask;
            Dependency dependency;

            Console.WriteLine("Enter 1 to add a new dependency, 2 to display a dependency by ID, 3 to display all the dependency , 4 to update dependency's details , 5 to delete or 0 to exit");
            choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
            //Dependency submenu
            switch (choice)
            {
                case 0:
                    break;
                case 1://create
                    Console.WriteLine("Enter a number of DependentTask");
                    _dependentTask = int.Parse(Console.ReadLine() ?? throw new Exception("you did not enter dependent task"));
                    Console.WriteLine("Enter number of DependsOnTask");
                    _dependsOnTask = int.Parse(Console.ReadLine() ?? throw new Exception("you did not enter _depends on task"));
                    dependency = new Dependency(0, _dependentTask, _dependsOnTask);
                    s_dalDependency?.Create(dependency);
                    Console.WriteLine("Created successfully");
                    break;
                case 2://read
                    Console.WriteLine("Enter an ID");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalDependency?.Read(_id);
                    break;
                case 3://read all
                    s_dalDependency?.ReadAll();
                    break;
                case 4://update
                    Console.WriteLine("Enter a number of DependentTask");
                    _dependentTask = int.Parse(Console.ReadLine() ?? throw new Exception("you did not enter dependent task"));
                    Console.WriteLine("Enter number of DependsOnTask");
                    _dependsOnTask = int.Parse(Console.ReadLine() ?? throw new Exception("you did not enter _depends on task"));
                    dependency = new Dependency(0, _dependentTask, _dependsOnTask);
                    s_dalDependency?.Update(dependency);
                    Console.WriteLine("successfully updated");
                    break;
                case 5://delete
                    Console.WriteLine("Enter task's id");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter an id"));
                    s_dalDependency?.Delete(_id);
                    Console.WriteLine("Deleted successfully");
                    break;
                default:
                    throw new Exception("Your choice is invalid");
            }
        }

        //Project management
        static void Main(string[] args)
        {
            try
            {
                int choice = 0;
                //Intilizate the project with some deta.
                Initialization.Do(s_dalEngineer, s_dalDependency, s_dalTask);
                Console.WriteLine("Enter 1 to engineer, 2 to task and 3 to dependency or 0 to exit");
                choice = int.Parse(Console.ReadLine() ?? throw new Exception("You did not enter a choice"));
                //The main menu
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
                    Console.WriteLine("Enter 1 to engineer, 2 to task and 3 to dependency or 0 to exit");
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