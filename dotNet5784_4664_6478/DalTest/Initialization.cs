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
        int moneEmail = 0;
        foreach (string _name in engineerNames)
        {
            double _cost=0;
            string _email;
            int _id;
            EngineerExperience _level=new EngineerExperience();
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);
            _email = engineerEmails[moneEmail++];
            _level =(EngineerExperience)s_rand.Next(0, 3); ;
            _cost= s_rand.Next(0, 10000);
            Engineer newEng = new(_id, _name, _email,_level,_cost);
            s_dalEngineer!.Create(newEng);
          
        }
    }
    private static void createDependency()
    {
    }
    private static void createTask()
    {
        string[] taskDescription =
        {
              "wash the dishes", "sweep the floor", "clean the room",
              "wash the floor", "arrange the books"
        };
        foreach (string _description in taskDescription)
        {
            bool _milestone;
            string? _alias;
            DateTime _CreatedAt;
            DateTime start;
            _alias = (s_rand.Next(0, 10000) % 2) == 0 ? _description + "ALIAS" : null;
             start = new DateTime(1995, 1, 1);
            _CreatedAt = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));

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
