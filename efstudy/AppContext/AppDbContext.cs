using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using efstudy.Entities;
namespace efstudy.AppContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Members { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=icxlCore;Integrated Security=True");
        }
    }
}
