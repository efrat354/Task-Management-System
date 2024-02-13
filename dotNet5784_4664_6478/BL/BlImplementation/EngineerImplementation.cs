namespace BlImplementation;
using BlApi;
using System.Net.Mail;
/// <summary>
/// A class that implements engineer
/// </summary>
internal class EngineerImplementation : IEngineer
{
    //a parameter which indicates correct class with which we want to implement the data layer
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// A method that find the engineer's task
    /// </summary>
    /// <param name="id">engineer's Id</param>
    /// <returns>nullable BO.TaskInEngineer object  which indicates engineer's task</returns>
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
    /// <summary>
    /// A function that checks whether the email is correct
    /// </summary>
    /// <param name="email">The email received</param>
    /// <returns>If the email is correct, true otherwise false</returns>
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
    /// <summary>
    /// The function checks the correctness of the engineer's details
    /// </summary>
    /// <param name="boEngineer">engineer object</param>
    /// <returns>If the information is correct, true otherwise false</returns>
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
    /// <summary>
    /// The function adds an engineer to the data
    /// </summary>
    /// <param name="boEngineer">The new engineer</param>
    /// <returns>The new engineer id</returns>
    /// <exception cref="BO.BlInvalidInput">Throw away if the details are incorrect</exception>
    /// <exception cref="BO.BlAlreadyExistsException">Throw if the engineer already exists</exception>
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
    /// <summary>
    /// An action that deletes an engineer from the data
    /// </summary>
    /// <param name="id">The engineer you want to remove</param>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the object you want to remove does not exist</exception>
    /// <exception cref="BO.BlDeletionImpossible">Throw if the engineer cannot be deleted</exception>
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
    /// <summary>
    /// A function that calls a certain object from the data
    /// </summary>
    /// <param name="id">The engineer's id you want to call</param>
    /// <returns>the desired engineer</returns>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the engineer does not exist in the data</exception>
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
    /// <summary>
    /// A function that reads all existing engineers either with or without conditions
    /// </summary>
    /// <param name="filter">A parameter by which you can filter which engineers you want</param>
    /// <returns>the desired list of engineers</returns>
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
    /// <summary>
    /// A function that updates a particular object with details
    /// </summary>
    /// <param name="boEngineer">Receives an engineer's object with updated details</param>
    /// <exception cref="BO.BlInvalidInput">Throw away if the details are incorrect</exception>
    /// <exception cref="BO.BlDoesNotExistException">Throw if the engineer does not exist in the data</exception>
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
