namespace DO;

public record Student
{
    int id;
    string Name;
    string? Alias = null;
    bool IsActive = false;
    DateTime? BirthDate = null;
}
