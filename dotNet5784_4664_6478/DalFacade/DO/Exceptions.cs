namespace DO;

[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}

//add a new exception class in order to take care of cases of invalid initialization such as creating a task with not active engineer etc. 
[Serializable]
public class DalInvalidInitialization : Exception
{
    public DalInvalidInitialization(string? message) : base(message) { }
}

//add a new exception class in order to take care of cases of invalid input such as entering nothing etc. 
[Serializable]
public class DalInvalidInput : Exception
{
    public DalInvalidInput(string? message) : base(message) { }
}



