﻿
namespace DalTest;
using DalApi;
using DO;
//Initialize the data
public static class Initialization
{
    //Static variable for the entities
    private static IDal? s_dal;
    //Random variable
    private static readonly Random s_rand = new();
    const int MIN_ID = 200000000;
    const int MAX_ID = 400000000;
    //Engineer Initialization
    private static void createEngineer()
    {
        //Declaration of variables
        double _cost = 0;
        string _email=" ";
        int _id;
        EngineerExperience _level = new EngineerExperience();//לבדוק שבאמת אפשר להגדיר את העצם פעם אחת
        Engineer newEng;
        //The data array of the names of the engineers 
        string[] engineerNames =
        {
              "Dani Levi", "Eli Amar", "Yair Cohen",
              "Ariela Levin", "Dina Klein", "Shira Israelof"
        };
        //The data array of the emails of the engineers
        string[] engineerEmails =
        {
              "DaniLevi@gmail.com", "EliAmar@gmail.com", "YairCohen@gmail.com",
              "ArielaLevin@gmail.com", "DinaKlein@gmail.com", "ShiraIsraelof@gmail.com"
        };
        //Go through a loop for each engineer in the array
        foreach (string _name in engineerNames)
        {
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer?.Read(_id) != null);

            foreach (string email in engineerEmails)
            {
                _email = email;
            }
            _level = (EngineerExperience)s_rand.Next(0, 3);
            _cost = s_rand.Next(0, 10000);
            newEng = new(_id, _name, _email, _level, _cost);
            s_dal!.Engineer?.Create(newEng);
        }
    }
    //Dependency Initialization
    private static void createDependency()
    {
        //Declaration of dependency's variable
        Dependency dependency;
        for (int i = 1; i < 5; i++)
        {
            dependency = new Dependency(0, i, i + 1);
            s_dal!.Dependency?.Create(dependency);//Calling the action create for each dependency
        }
    }
    //Task's Initialization
    private static void createTask()
    {
   
       // Declaration of variables
        string _alias, _product, _remarks;
        int _engineerId, count=0;
        DateTime _createdAtDate, _startDate, _scheduledDate, _completeDate;//_deadlineDate,
        TimeSpan _requiredEffortTime;
        Task task;
        EngineerExperience _complexity = new EngineerExperience();

        //The data array of the names of the tasks
        string[] taskDescription =
        {
             "wash the dishes", "sweep the floor", "wash the floor", "clean the room",
               "arrange the books"
        };
       // The data array of the products of the engineers
        string[] taskProduct =
        {
            "clean dishes", "floor without dirt","clean floor","tidy room","books' pile"
        };
        //The data array of the remarks of the engineers
        string[] taskRemark =
        {
            "The task was difficult","the task was easy","There were black marks on the floor","the task took long time","the task was very easy"
        };
        //Go through a loop for each dependency in the array
        foreach (string _description in taskDescription)
        {
            _alias = _description + "ALIAS";
            _requiredEffortTime = new TimeSpan(s_rand.Next(1, 60));
             _createdAtDate = DateTime.Now;
            _startDate = _createdAtDate.AddDays(s_rand.Next(1,10));
            _scheduledDate = _startDate.AddDays(7);
            _completeDate = _startDate.AddDays(s_rand.Next(1, 12));
            _product = taskProduct[count];
            _remarks= taskRemark[count];
            var engineerList = s_dal!.Engineer?.ReadAll();
            _engineerId = (engineerList?.ToList() ?? throw new DalInvalidInitialization("There are not exist engineers, cannot be created"))[count]!.Id;
            _complexity = (EngineerExperience)s_rand.Next(0, 5);
            task = new Task(0, _alias, _description, _createdAtDate,_requiredEffortTime,false, _complexity, _startDate, _scheduledDate, null, _completeDate, _product, _remarks, _engineerId);
            s_dal!.Task?.Create(task);//Calling the action create for each dependency
            count++;
        }
    }
    //A function that calls all the initialization functions
    public static void Do() 
    {
        //s_dal = dal ?? throw new DalInvalidInitialization("DAL object can not be null!"); 
        s_dal = DalApi.Factory.Get;
        createEngineer();
        createTask();
        createDependency();
    }
}
