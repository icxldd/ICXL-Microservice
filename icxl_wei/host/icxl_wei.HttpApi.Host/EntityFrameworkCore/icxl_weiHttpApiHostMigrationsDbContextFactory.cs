using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace icxl_wei.EntityFrameworkCore
{
    public class icxl_weiHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<icxl_weiHttpApiHostMigrationsDbContext>
    {
        public icxl_weiHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<icxl_weiHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("icxl_wei"));

            return new icxl_weiHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
