namespace BO;

/// <summary>
/// Class that include generic tools for global using in the project
/// </summary>
public static class Tools//punlic??
{
    /// <summary>
    /// Function that print all types of objects 
    /// </summary>
    /// <param name="ob">The object for print</param>
    /// <returns>The retunr is the object as a string</returns>
    public static string genericToString(this object ob)
    {
        var prop = ob.GetType().GetProperties();
        string s = "";
        foreach (var property in prop)
        {
            s += property.Name + ": "+ property.GetValue(ob) + "\n";//איך להמיר את הDEPNDENCIES MILSTONE
        }
        return s;
    }

}
