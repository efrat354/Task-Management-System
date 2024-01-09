namespace Dal;
//Class that responsible to promote the running id's
internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
   
    internal static DateTime? startProjectDate = new DateTime(2024, 1, 1);
   
    internal static DateTime? endProjectDate = new DateTime(2030, 1, 1);
}
