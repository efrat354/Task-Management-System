﻿namespace BlApi;
/// <summary>
/// Engineer interface
/// </summary>
public interface IEngineer
{
    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer?, bool>? filter = null);
    public BO.Engineer Read(int id);
    public int Create(BO.Engineer eng);
    public void Delete(int id);
    public void Update(BO.Engineer eng);
}
