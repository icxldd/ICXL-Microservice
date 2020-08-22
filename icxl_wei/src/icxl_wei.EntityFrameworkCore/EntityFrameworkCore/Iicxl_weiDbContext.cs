using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace icxl_wei.EntityFrameworkCore
{
    [ConnectionStringName(icxl_weiDbProperties.ConnectionStringName)]
    public interface Iicxl_weiDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}