using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Create(BO.Engineer eng)
    {
        DO.Engineer doEngineer = new DO.Engineer
               (boEngineer.Id, boStudent.Name, boStudent.Alias, boStudent.IsActive, boStudent.BirthDate);
        try
        {
            int idStud = _dal.Student.Create(doStudent);
            return idStud;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer eng)
    {
        throw new NotImplementedException();
    }
}
