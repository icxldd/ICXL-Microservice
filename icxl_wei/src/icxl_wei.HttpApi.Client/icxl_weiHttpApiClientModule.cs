using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace icxl_wei
{
    [DependsOn(
        typeof(icxl_weiApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class icxl_weiHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "icxl_wei";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(icxl_weiApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
