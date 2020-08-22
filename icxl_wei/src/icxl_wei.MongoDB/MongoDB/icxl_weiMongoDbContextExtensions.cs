using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace icxl_wei.MongoDB
{
    public static class icxl_weiMongoDbContextExtensions
    {
        public static void Configureicxl_wei(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new icxl_weiMongoModelBuilderConfigurationOptions(
                icxl_weiDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}