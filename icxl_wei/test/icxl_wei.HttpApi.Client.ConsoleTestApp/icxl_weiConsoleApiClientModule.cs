using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class icxl_weiConsoleApiClientModule : AbpModule
    {
        
    }
}
