using BlApi;
using BO;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {

        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost, boEngineer.Active);
        //try
        //{
        int idEng = _dal.Engineer.Create(doEngineer);
        return idEng;
        // }
        // catch (DO.DalAlreadyExistsException ex)
        // {
        // throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        // }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch
        {

        }
    }

    public BO.Engineer Read(int id)
    {
        TaskInEngineer taskInEngineer;
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
        {
            // throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        }

        DO.Task? t = _dal.Task.ReadAll().FirstOrDefault(t => t?.EngineerId == id);
        if (t == null)
        {
            taskInEngineer = new TaskInEngineer() { Alias = " " };
        }
        else
        {
            taskInEngineer = new TaskInEngineer() { Id = t.Id, Alias = t.Alias };
        }
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,//למה
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Active = doEngineer.Active,
            Task = taskInEngineer
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {

        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost
                    //צריך task?
                });
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
