using Volo.Abp.Reflection;

namespace icxl_wei.Permissions
{
    public class icxl_weiPermissions
    {
        public const string GroupName = "icxl_wei";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(icxl_weiPermissions));
        }
    }
}