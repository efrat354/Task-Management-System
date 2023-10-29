namespace DO;

public record Task
{
    string Description;
    string Alias;
    bool Milestone;
    DateTime CreatedAt;
    DateTime Start;
    DateTime ScheduledDate;
    DateTime ForcastDate;
    DateTime Deadline;
    DateTime Complete;
    string Deliverables;
    string Remarks;
    int EngineerId;
    EngineerExperience ComplexityLevel;
}
