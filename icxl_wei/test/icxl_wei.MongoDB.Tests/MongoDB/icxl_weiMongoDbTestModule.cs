using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace icxl_wei.MongoDB
{
    [DependsOn(
        typeof(icxl_weiTestBaseModule),
        typeof(icxl_weiMongoDbModule)
        )]
    public class icxl_weiMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}