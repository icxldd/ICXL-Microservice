using icxl_wei.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace icxl_wei.Permissions
{
    public class icxl_weiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(icxl_weiPermissions.GroupName, L("Permission:icxl_wei"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<icxl_weiResource>(name);
        }
    }
}