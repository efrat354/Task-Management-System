

using BlApi;
using BO;
using System.Collections.Generic;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void CreateSchedule()
    {
        var dependenciesList = _dal.Dependency.ReadAll();
                                from dep in dependenciesList
                                group dep by dep.DependentTask into groups
                                select new { groups.Key,list= groups.OrderBy()  };
    }

    public Milestone Read(int id)
    {
        throw new NotImplementedException();
    }

    public Milestone Update(int id)
    {
        throw new NotImplementedException();
    }
}
