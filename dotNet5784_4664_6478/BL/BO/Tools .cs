using System.Text;

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
        string str = "";
        if (ob == null) { return str; }
        Type type = ob.GetType();
        foreach (var property in type.GetProperties())
        {
            var value = property.GetValue(ob);
            if (value != null && value is IEnumerable<object>)
            {
                str += property.Name + ": ";
                foreach (var property2 in (value as IEnumerable<object>)!)
                    str += property2.ToString();
            }
            else
                str += property.Name + ": " + value + "\n";
        }
        return str;
    }

    
}
