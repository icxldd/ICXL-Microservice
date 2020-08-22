using Localization.Resources.AbpUi;
using icxl_wei.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class icxl_weiHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(icxl_weiHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<icxl_weiResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
