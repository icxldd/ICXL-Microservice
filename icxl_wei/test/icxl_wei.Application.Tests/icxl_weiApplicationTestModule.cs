using Volo.Abp.Modularity;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiApplicationModule),
        typeof(icxl_weiDomainTestModule)
        )]
    public class icxl_weiApplicationTestModule : AbpModule
    {

    }
}
