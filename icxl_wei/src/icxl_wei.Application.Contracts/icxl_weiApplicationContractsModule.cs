using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class icxl_weiApplicationContractsModule : AbpModule
    {

    }
}
