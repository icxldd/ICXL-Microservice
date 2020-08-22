using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace icxl_wei.EntityFrameworkCore
{
    public static class icxl_weiDbContextModelCreatingExtensions
    {
        public static void Configureicxl_wei(
            this ModelBuilder builder,
            Action<icxl_weiModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new icxl_weiModelBuilderConfigurationOptions(
                icxl_weiDbProperties.DbTablePrefix,
                icxl_weiDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }
    }
}