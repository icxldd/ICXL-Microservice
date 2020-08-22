using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace icxl_wei.EntityFrameworkCore
{
    public class icxl_weiHttpApiHostMigrationsDbContext : AbpDbContext<icxl_weiHttpApiHostMigrationsDbContext>
    {
        public icxl_weiHttpApiHostMigrationsDbContext(DbContextOptions<icxl_weiHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configureicxl_wei();
        }
    }
}
