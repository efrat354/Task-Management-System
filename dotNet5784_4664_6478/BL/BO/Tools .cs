namespace BO;

public static class Tools//punlic??
{
    public static string genericToString(this object ob)
    {
        var prop = ob.GetType().GetProperties();
        string s = "";
        foreach (var property in prop)
        {
            s += property.Name + ":"+ property.GetValue(ob);
        }
        return s;
    }

}
