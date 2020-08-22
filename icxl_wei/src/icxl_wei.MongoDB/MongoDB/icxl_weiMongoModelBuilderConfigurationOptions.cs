using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace icxl_wei.MongoDB
{
    public class icxl_weiMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public icxl_weiMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}