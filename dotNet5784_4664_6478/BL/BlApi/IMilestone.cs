﻿

namespace BlApi;

public interface IMilestone
{
    public void CreateSchedule();
    public BO.Milestone Read(int id);
    public BO.Milestone Update(int id);
}
