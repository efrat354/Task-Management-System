using BlApi;
using BO;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private TaskInEngineer? FindTask(int id)
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
    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id<=0)
        {
            throw new BO.BlInvalidInput("Id is not valid");
        }
        if (boEngineer.Name != "")
        {
            throw new BO.BlInvalidInput("Name is not valid");
        }
        if (!IsValidEmail(boEngineer.Email))
        {
            throw new BO.BlInvalidInput("Email is not valid");
        }
        if (boEngineer.Cost <= 0)
        {
            throw new BO.BlInvalidInput("Cost is not valid");
        }

        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost, boEngineer.Active);
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

    public void Delete(int id)
    {
        //האם צריך לבדוק פה האם המהנדס קיים כי אם הוא לוחץ עליו למחוק אותו אז הוא בטוח קיים
        BO.Engineer boEngineer = Read(id);
        if (boEngineer.Task!=null)
        {
            try
            {
                _dal.Engineer.Delete(id);
            }
            catch (BO.BlDoesNotExistException ex) { }
            {
                // והאם צריך לעשות פה שגיאה מסוג מחיקה אןו לא קיים למעלה
            }
        }
        else
        {
            throw new BO.BlDeletionImpossible($"Engineer with ID={id} can not be deleted");
        }

    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
        {
             throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        }

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Active = doEngineer.Active,
            Task = FindTask(id)
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)//לבדוק איך לשלוח כי הוא יכול להיות null
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = FindTask(doEngineer.Id)
                    //??Active
                }); ; 
    }

    public void Update(BO.Engineer eng)
    {
        DO.Engineer? reference = _dal.Engineer.Read(eng.Id);
        if (reference != null)
        {
            _dal.Engineer.Update(reference);
        }
        //else{
        //}


    }
}
