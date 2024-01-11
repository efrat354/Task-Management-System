namespace BlImplementation;
using BlApi;
using System.Net.Mail;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.TaskInEngineer? FindTask(int id)
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
    private string Validation(BO.Engineer boEngineer)
    {
        if (boEngineer.Id <= 0)
        {
            return "Id is not valid";
        }
        if (boEngineer.Name == "")
        {
            return "Name is not valid";
        }
        if (!IsValidEmail(boEngineer.Email))
        {
            return "Email is not valid";
        }
        if (boEngineer.Cost <= 0)
        {
            return "Cost is not valid";
        }
        else
        {
            return "";
        }
    }
    public int Create(BO.Engineer boEngineer)
    {
        string message = Validation(boEngineer);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
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
        BO.TaskInEngineer? taskInEngineer = FindTask(id);
        if (taskInEngineer == null)
        {
            try
            {
                _dal.Engineer.Delete(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exists", ex);
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
            Task = FindTask(id)
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer?, bool>? filter = null)
    {
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
               select new BO.Engineer()
               {
                   Id = doEngineer.Id,
                   Name = doEngineer.Name,
                   Email = doEngineer.Email,
                   Level = (BO.EngineerExperience)doEngineer.Level,
                   Cost = doEngineer.Cost,
                   Task = FindTask(doEngineer.Id)
               };
    }

    public void Update(BO.Engineer boEngineer)
    {
        string message = Validation(boEngineer);
        if (message != "")
        {
            throw new BO.BlInvalidInput(message);
        }
        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does not exists", ex);
        }
    }
}
