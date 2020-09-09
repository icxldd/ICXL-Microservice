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
using icxl_api.Domain.Entities;

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
                    a.Name = "icxl";
                    a.PassWord = "123456";
                    dbContext.Account.Add(a);
                    dbContext.SaveChanges();
                }

                if (dbContext.Menu.Count() == 0)
                {
                    Menu a = new Menu();
                    a.id = Guid.NewGuid().ToString();
                    a.name = "系统管理";
                    a.parentId = "";

                    Menu aa = new Menu();
                    aa.id = Guid.NewGuid().ToString();
                    aa.name = "人员管理";
                    aa.parentId = a.id;

                    Menu aaa = new Menu();
                    aaa.id = Guid.NewGuid().ToString();
                    aaa.name = "权限管理";
                    aaa.parentId = a.id;




                    Menu a1 = new Menu();
                    a1.id = Guid.NewGuid().ToString();
                    a1.name = "业务管理";
                    a1.parentId = "";

                    Menu aa1 = new Menu();
                    aa1.id = Guid.NewGuid().ToString();
                    aa1.name = "订单管理";
                    aa1.parentId = a1.id;

                    Menu aaa1 = new Menu();
                    aaa1.id = Guid.NewGuid().ToString();
                    aaa1.name = "流程管理";
                    aaa1.parentId = a1.id;




                    dbContext.Menu.Add(a);
                    dbContext.Menu.Add(aa);
                    dbContext.Menu.Add(aaa);
                    dbContext.Menu.Add(a1);
                    dbContext.Menu.Add(aa1);
                    dbContext.Menu.Add(aaa1);
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
