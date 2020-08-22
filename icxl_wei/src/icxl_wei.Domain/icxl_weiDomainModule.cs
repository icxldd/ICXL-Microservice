using Volo.Abp.Modularity;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiDomainSharedModule)
        )]
    public class icxl_weiDomainModule : AbpModule
    {

    }
}
