namespace DO;
//A file for defining different classes of exception types
//Add a new exception class in order to take care of a case of an entity with an ID number that does not exist in the list
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
//Add a new exception class in order to take care of a case an entity with an ID number that already exists in the list

[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
//Add a new exception class in order to take care of cases that cannot be deleted such as a task that has other tasks that depend on it

[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}

//Add a new exception class in order to take care of cases of invalid initialization such as creating a task with not active engineer etc. 
[Serializable]
public class DalInvalidInitialization : Exception
{
    public DalInvalidInitialization(string? message) : base(message) { }
}

//Add a new exception class in order to take care of cases of invalid input such as entering nothing etc. 
[Serializable]
public class DalInvalidInput : Exception
{
    public DalInvalidInput(string? message) : base(message) { }
}



