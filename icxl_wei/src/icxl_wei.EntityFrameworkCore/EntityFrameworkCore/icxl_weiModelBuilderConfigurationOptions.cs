using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace icxl_wei.EntityFrameworkCore
{
    public class icxl_weiModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public icxl_weiModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}