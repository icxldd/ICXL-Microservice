using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiDomainModule),
        typeof(icxl_weiApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class icxl_weiApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<icxl_weiApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<icxl_weiApplicationModule>(validate: true);
            });
        }
    }
}
