namespace DalTest;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();
    const int MIN_ID = 200000000;
    const int MAX_ID = 400000000;
    private static void createEngineer()
    {
        double _cost = 0;
        string _email=" ";
        int _id;
        EngineerExperience _level = new EngineerExperience();//לבדוק שבאמת אפשר להגדיר את העצם פעם אחת
        Engineer newEng; 

        string[] engineerNames =
        {
              "Dani Levi", "Eli Amar", "Yair Cohen",
              "Ariela Levin", "Dina Klein", "Shira Israelof"
        };

        string[] engineerEmails =
        {
              "DaniLevi@gmail.com", "EliAmar@gmail.com", "YairCohen@gmail.com",
              "ArielaLevin@gmail.com", "DinaKlein@gmail.com", "ShiraIsraelof@gmail.com"
        };

        //int moneEmail = 0;
        foreach (string _name in engineerNames)
        {
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);

            foreach (string email in engineerEmails)
            {
                _email = email;
            }
            // _email = engineerEmails[moneEmail++];
            _level = (EngineerExperience)s_rand.Next(0, 3); ;
            _cost = s_rand.Next(0, 10000);
            newEng = new(_id, _name, _email, _level, _cost);
            s_dalEngineer!.Create(newEng);
        }
    }
    private static void createDependency()
    {
        Dependency dependency;
        for (int i = 1; i < 5; i++)
        {
            dependency = new Dependency(0, i, i + 1);//הת.ז ראשון ולכן נשלח לו ערך ברירת מחדל ואז בcreate  זה ישנה את זה
            s_dalDependency?.Create(dependency);
        }
    }
    private static void createTask()
    {
        //int Id = 0,
        //string? Description = null, V
        //string? Alias = null,V
        //bool Milestone = false,
        //DateTime CreatedAt = new DateTime() ,V
        //DateTime? Start = null,
        //DateTime? ScheduledDate = null,
        //DateTime? ForcastDate = null,
        //DateTime? Deadline = null,
        //DateTime? Complete = null,
        //string? Deliverables = null,
        //string? Remarks = null,
        //int? EngineerId = null,
        //EngineerExperience ComplexityLevel = 0
        string? _alias, _deliverables, _remarks;
        int range, _engineerId;
        DateTime _createdAt,_start, _scheduledDate, _forcastDate, _deadline, _complete;
        Task task;
        EngineerExperience _complexityLevel = 0;

        string[] taskDescription =
        {
              "wash the dishes", "sweep the floor", "wash the floor", "clean the room",
               "arrange the books"
        };

        foreach (string _description in taskDescription)
        {
            _alias = (s_rand.Next(0, 10000) % 2) == 0 ? _description + "ALIAS" : null;
            _createdAt = DateTime.Now;
            _start = _createdAt.AddDays(4);
            range = (DateTime.Today - _start).Days;
            DateTime _bdt = _start.AddDays(s_rand.Next(range));
            task = new Task(0, _description, _alias,false, _createdAt, _start, _scheduledDate, _forcastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);
            s_dalTask?.Create(task);
        }
    }

    public static void Do(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createTask();
        createDependency();
        createEngineer();
    }

}
