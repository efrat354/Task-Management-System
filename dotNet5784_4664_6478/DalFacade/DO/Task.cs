﻿namespace DO;

public record Task
(
    string? Description=null,
    string? Alias=null,
    bool Milestone=false,
    DateTime CreatedAt=new DateTime() ,
    DateTime? Start=null,
    DateTime? ScheduledDate = null,
    DateTime? ForcastDate = null,
    DateTime? Deadline = null,
    DateTime? Complete = null  ,
    string? Deliverables=null,
    string? Remarks = null,
    int? EngineerId = null
    //EngineerExperience ComplexityLevel

);
