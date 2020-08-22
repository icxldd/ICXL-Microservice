using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace icxl_wei.MongoDB
{
    [ConnectionStringName(icxl_weiDbProperties.ConnectionStringName)]
    public class icxl_weiMongoDbContext : AbpMongoDbContext, Iicxl_weiMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Configureicxl_wei();
        }
    }
}