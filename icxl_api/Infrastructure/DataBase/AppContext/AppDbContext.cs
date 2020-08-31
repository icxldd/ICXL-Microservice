using icxl_api.Domain.Entities;
using icxl_api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icxl_api.AppContext
{
    public class AppDbContext : DbContext
    {
        #region 构造函数
        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        #endregion

        public DbSet<Account> Account { get; set; }

        public DbSet<Menu> Menu { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=icxlCore;Integrated Security=True");
        //}
    }
}
