using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace icxl_wei.EntityFrameworkCore
{
    [ConnectionStringName(icxl_weiDbProperties.ConnectionStringName)]
    public class icxl_weiDbContext : AbpDbContext<icxl_weiDbContext>, Iicxl_weiDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public icxl_weiDbContext(DbContextOptions<icxl_weiDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configureicxl_wei();
        }
    }
}