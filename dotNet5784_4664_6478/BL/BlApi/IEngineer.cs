namespace BlApi;

public interface IEngineer
{
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null);//האם צריך EngineerInList?
    public BO.Engineer Read(int id);
    public int Create(BO.Engineer eng);//האם צריך להחזיר id?
    public void Delete(int id);
    public void Update(BO.Engineer eng);
}
