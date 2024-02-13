namespace BlApi;
/// <summary>
/// Factory interface
/// </summary>
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl();
}
