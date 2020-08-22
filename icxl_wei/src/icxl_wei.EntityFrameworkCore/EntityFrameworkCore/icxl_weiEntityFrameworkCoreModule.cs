using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace icxl_wei.EntityFrameworkCore
{
    [DependsOn(
        typeof(icxl_weiDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class icxl_weiEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<icxl_weiDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}