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
            services.AddMvc(options =>
            {
                //options.Filters.Add<XcActionFilter>();
            });
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfigs>());
            services.AddAutoMapper();


            services.AddMvcCore().AddAuthorization().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //ignore Entity framework Navigation property back reference problem. Blog >> Posts. Post >> Blog. Blog.post.blog will been ignored.
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddJsonFormatters();

            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();
                x.UseRabbitMQ(mq =>
                {
                    mq.HostName = "192.168.99.100";
                    mq.Port = 5672;
                    mq.UserName = "guest";
                    mq.Password = "guest";
                });
                x.UseDashboard();
            });

            services.AddEntityFrameworkNpgsql();
            var connStr = Configuration["ConnectionString"];
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connStr));

            services.Configure<AppConfig>(Configuration);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));
            Console.WriteLine("http://" + Configuration["idsUrl:IP"] + ":" + Configuration["idsUrl:Port"]);
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://" + Configuration["idsUrl:IP"] + ":" + Configuration["idsUrl:Port"];
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyTestService", Version = "v1" });
            });

            //services.AddScoped<IAccountRepository, AccountRepository>();

            //初始化容器
            var builder = new ContainerBuilder();
            //管道寄居
            builder.Populate(services);
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            //构造
            ApplicationContainer = builder.Build();
            //将AutoFac反馈到管道中
            return new AutofacServiceProvider(ApplicationContainer);
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
