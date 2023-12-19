﻿namespace Dal;
using DalApi;
using System;

/// <summary>
/// Class that use IDal by creating 3 implementations each one for an one entity 
///And creation dates for th ebegining and the end of the project. In addition it implements the reset function
/// </summary>

sealed public class DalList : IDal
{
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public DateTime? StartProjectDate => throw new NotImplementedException();

    public DateTime? EndProjectDate => throw new NotImplementedException();

    public void Reset()
    {
        Task.Reset();
        Engineer.Reset();
        Dependency.Reset();
    }
}
