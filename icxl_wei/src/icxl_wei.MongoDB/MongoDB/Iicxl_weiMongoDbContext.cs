using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace icxl_wei.MongoDB
{
    [ConnectionStringName(icxl_weiDbProperties.ConnectionStringName)]
    public interface Iicxl_weiMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
