
namespace BO;
//A file for defining different classes of exception types
//Add a new exception class in order to take care of a case of an entity with an ID number that does not exist in the list
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

//Add a new exception class in order to take care of a case an entity with an ID number that already exists in the list

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

//Add a new exception class in order to take care of cases that cannot be deleted such as a task that has other tasks that depend on it

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
               : base(message, innerException) { }
}

//Add a new exception class in order to take care of cases of invalid initialization such as creating a task with not active engineer etc. 
[Serializable]
public class BlInvalidInitialization : Exception
{
    public BlInvalidInitialization(string? message) : base(message) { }
    public BlInvalidInitialization(string message, Exception innerException)
               : base(message, innerException) { }
}

[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string message, Exception innerException)
               : base(message, innerException) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}


//Add a new exception class in order to take care of cases of invalid input such as entering nothing etc. 
[Serializable]
public class BlInvalidInput : Exception
{
    public BlInvalidInput(string? message) : base(message) { }
}