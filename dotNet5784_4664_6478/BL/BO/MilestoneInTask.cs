﻿namespace BO;

public class MilestoneInTask
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => this.genericToString();
}
