using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using icxl_api.AppContext;
using icxl_api.Entities;
using icxl_api.Filter;
using icxl_api.IRepository;
using icxl_api.Repository;
using icxl_api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using icxl_api.Infrastructure.ServiceCollection;
namespace icxl_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public static IContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var connStr = Configuration["ConnectionString"];
            services.AddMvc(options =>
            {
                //options.Filters.Add<logActionFilter>();
            });

            services.AddMvcCore().AddAuthorization().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //ignore Entity framework Navigation property back reference problem. Blog >> Posts. Post >> Blog. Blog.post.blog will been ignored.
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddJsonFormatters();
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connStr));
            services.Configure<AppConfig>(Configuration);
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));
            services._AddAutoMapper();
            services._AddCap();
            services._AddAuthentication(Configuration["idsUrl:IP"], Configuration["idsUrl:Port"]);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyTestService", Version = "v1" });
            });

            return services._AddAutoFac(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime lifetime)
        {
            app.UseStatusCodePages();
            app.UseAuthentication();
            var settingsOptions = serviceProvider.GetService<IOptions<AppConfig>>();
            var appConfig = settingsOptions.Value;
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
          {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
          });

            app.RegisterConsul(lifetime, appConfig);
            app.UseMvc();



            #region Database Init
            {
                var dbContext = serviceProvider.GetService<AppDbContext>();
                dbContext.Database.Migrate();
                if (dbContext.Account.Count() == 0)
                {
                    Account a = new Account();
                    a.Id = "123";
                    a.name = "icxl";
                    a.password = "123456";
                    dbContext.Account.Add(a);
                    dbContext.SaveChanges();
                }
            }
            #endregion

            app.UseCap();

        }
    }

    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}
